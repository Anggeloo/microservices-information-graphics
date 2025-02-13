using microservices_information_graphics.Database;
using microservices_information_graphics.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true) 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); 
    });
});

builder.WebHost.UseUrls("http://*:4000");

var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("MYSQL_CONNECTION_STRING environment variable and DefaultConnection in appsettings.json are both missing.");
}

builder.Services.AddDbContext<DBContext>(options =>
    options.UseMySQL(connectionString));


builder.Services.AddScoped<InformationGraphicService>();


builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.MapControllers();


app.UseSwagger();
app.UseSwaggerUI();

app.Run();
