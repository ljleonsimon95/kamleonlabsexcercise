using System.Reflection;
using KamaleonlabsExcercise.AppDbContext;
using KamaleonlabsExcercise.Features.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace KamaleonlabsExcercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<NewsDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(10, 11, 9))));


            RegisterAllHandlerServices(builder);


            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Kamaleonlabs",
                    Description = "News service",
                });
                options.OperationFilter<FileUploadSchemaFilter >(); // Agrega el filtro para IFormFile
            });

            var app = builder.Build();

            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Kamaleonlabs v1");
                });
            }

            app.MapControllers();

            app.Run();
        }

        private static void RegisterAllHandlerServices(WebApplicationBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var handlerTypes = assembly
                .GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(CommandHandlerAttribute), true).Length > 0);

            foreach (var handler in handlerTypes)
            {
                var useCaseInterfaces = handler
                    .GetInterfaces()
                    .Where(i => !i.IsGenericType)
                    .Where(i => Array.Exists(i.GetInterfaces(), ImplementsAUseCase));

                foreach (var iface in useCaseInterfaces)
                {
                    builder.Services.AddScoped(iface, handler);
                }
            }
        }

        private static bool ImplementsAUseCase(Type type) =>
            type.IsGenericType
            && (
                type.GetGenericTypeDefinition() == typeof(IUseCase<>)
                || type.GetGenericTypeDefinition() == typeof(IUseCase<,>)
            );
    }
}