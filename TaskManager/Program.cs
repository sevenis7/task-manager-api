using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Middleware;
using TaskManager.Models;
using TaskManager.Models.Auth;
using TaskManager.Services;
using TaskManager.Services.APIService;
using TaskManager.Services.Auth;
using TaskManager.Services.Mappers;
using TaskManager.Services.Mappers.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            builder.Services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Task Manager API",
                    Version = "v1",
                    Description = "API дл€ управлени€ задач с JWT авторизацией"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "¬ведите JWT токен",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type  = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }
                        ,
                        new string[] {}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            }); 

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
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddTransient<IJwtService, JwtService>();
            builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!))
                    };
                });

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
