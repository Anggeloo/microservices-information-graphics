using microservices_information_graphics.Database;
using microservices_information_graphics.Models;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

namespace microservices_information_graphics.Services
{
    public class InformationGraphicService
    {
        private readonly DBContext _context;

        public InformationGraphicService(DBContext context)
        {
            _context = context;
        }

        public async Task<List<InformationGraphic>> GetAllAsync()
        {
            return await _context.InformationGraphic.Where(x => x.Status == true).ToListAsync();
        }

        public async Task<InformationGraphic> GetByCodeAsync(string codice)
        {
            return await _context.InformationGraphic.FirstOrDefaultAsync(order => order.GraphicCode == codice && order.Status == true);
        }

        public async Task<InformationGraphic> CreateAsync(InformationGraphic model)
        {
            model.Status = true;
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            var createOrder = _context.InformationGraphic.Add(model);
            await _context.SaveChangesAsync();
            var id = createOrder.Entity.GraphicId;

            var result = await _context.InformationGraphic.FirstOrDefaultAsync(x => x.GraphicId == id);

            return result;
        }

        public async Task<InformationGraphic> UpdateAsync(string codice, InformationGraphic model)
        {
            var result = await _context.InformationGraphic.FirstOrDefaultAsync(x => x.GraphicCode == codice);

            if (result == null)
            {
                return null;
            }

            result.UpdatedAt = DateTime.Now;
            result.GraphicType = model.GraphicType;
            result.Title = model.Title;
            result.Description = model.Description;
            result.UserCreatedBy = model.UserCreatedBy;

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<InformationGraphic> DeleteAsync(string codice)
        {
            var result = await _context.InformationGraphic.FirstOrDefaultAsync(x => x.GraphicCode == codice);

            if (result == null)
            {
                return null;
            }

            result.Status = false;
            await _context.SaveChangesAsync();
            return result;
        }


        public async Task<bool> CheckIfExistsAsync(string codice)
        {
            var exist = await _context.InformationGraphic.FirstOrDefaultAsync(x => x.GraphicCode == codice);
            if (exist == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<string> GenerateNextOrderCodeAsync()
        {
            var quantity = await _context.InformationGraphic.CountAsync();
            var nextCode = $"INFG_{quantity + 1}";
            return nextCode;
        }
    }


}
