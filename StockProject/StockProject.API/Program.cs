using Microsoft.EntityFrameworkCore;
using StockProject.Repositories.Abstract;
using StockProject.Repositories.Concrete;
using StockProject.Repositories.Context;
using StockProject.Service.Abstract;
using StockProject.Service.Concrete;

namespace StockProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StockProjectContext>(options => {
                options.UseSqlServer("Server=LAPTOP-5JQ1KEF4;Database=DBStock_Project;Trusted_Connection=True;");
                
             });
            builder.Services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));

            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}