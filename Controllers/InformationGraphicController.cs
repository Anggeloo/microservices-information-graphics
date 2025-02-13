using microservices_information_graphics.Models;
using microservices_information_graphics.Services;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;

[ApiController]
[Route("[controller]")]
public class InformationGraphicController : Controller
{
    private readonly InformationGraphicService _service;

    public InformationGraphicController(InformationGraphicService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await _service.GetAllAsync();
        return Ok(new ApiResponse<List<InformationGraphic>>("success", result, "List of informations graphic"));
    }

    [HttpGet("{codice}")]
    public async Task<IActionResult> GetOrderByCode(string codice)
    {

        var result = await _service.GetByCodeAsync(codice);

        if (result == null)
        {
            return Ok(new ApiResponse<InformationGraphic>("empty", result, "Informations graphic not found"));
        }

        return Ok(new ApiResponse<InformationGraphic>("success", result, "Informations graphic found"));
    }

    [HttpPost("add")]
    public async Task<IActionResult> Create([FromBody] InformationGraphic model)
    {
        if (model == null)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "Invalid informations graphic data"));
        }


        model.GraphicCode = await _service.GenerateNextOrderCodeAsync();

        var created = await _service.CreateAsync(model);

        if (created == null)
        {
            return StatusCode(500, new ApiResponse<string>("Error", null, "Informations graphic was created but could not be retrieved"));
        }

        return CreatedAtAction(nameof(Create),
            new { codice = created.GraphicCode },
            new ApiResponse<InformationGraphic>("success", created, "Information graphic created successfully"));
    }

    [HttpPut("update/{codice}")]
    public async Task<IActionResult> CreateOrder(string codice, [FromBody] InformationGraphic model)
    {
        if (model == null)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "Invalid Information graphic data"));
        }

        var exits= await _service.CheckIfExistsAsync(codice);

        if (exits == false)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "The graphic code does not exist"));
        }

        var updated = await _service.UpdateAsync(codice, model);

        if (updated == null)
        {
            return StatusCode(400, new ApiResponse<string>("Error", null, "Information graphic was updated but could not be retrieved"));
        }

        return Ok(new ApiResponse<InformationGraphic>("success", updated, "Information graphic updated successfully"));
    }

    [HttpDelete("delete/{codice}")]
    public async Task<IActionResult> DeñeteOrder(string codice)
    {
        var exitsOrder = await _service.CheckIfExistsAsync(codice);

        if (exitsOrder == false)
        {
            return BadRequest(new ApiResponse<string>("Error", null, "The graphic code does not exist"));
        }

        var delete = await _service.DeleteAsync(codice);

        if (delete == null)
        {
            return StatusCode(400, new ApiResponse<string>("Error", null, "Error ..."));
        }

        return Ok(new ApiResponse<InformationGraphic>("success", delete, "Information graphic deleted successfully"));
    }
}

