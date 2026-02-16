using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.Services.APIService;
using TaskManager.Services.Mappers;
using TaskManager.Services.Mappers.Interfaces;

namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddScoped<ITaskService,TaskService>();
            builder.Services.AddScoped<ITaskApiService, TaskApiService>();
            builder.Services.AddTransient<IEntityMapper<TaskItem, TaskDto>, TaskMapper>();
            builder.Services.AddTransient<IEntityMapper<StatusItem, StatusDto>, StatusMapper>();
            builder.Services.AddTransient<IEntityMapper<CategoryItem, CategoryDto>, CategoryMapper>();
            builder.Services.AddTransient<IEntityMapper<PriorityItem, PriorityDto>, PriorityMapper>();
            builder.Services.AddScoped<IBaseService<StatusItem, StatusItemModel>, StatusService>();
            builder.Services.AddScoped<IBaseApiService<StatusItemModel, StatusDto>, StatusApiService>();
            builder.Services.AddScoped<IBaseService<CategoryItem, CategoryItemModel>, CategoryService>();
            builder.Services.AddScoped<IBaseApiService<CategoryItemModel, CategoryDto>, CategoryApiService>();
            builder.Services.AddScoped<IBaseService<PriorityItem, PriorityItemModel>, PriorityService>();
            builder.Services.AddScoped<IBaseApiService<PriorityItemModel, PriorityDto>, PriorityApiService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
