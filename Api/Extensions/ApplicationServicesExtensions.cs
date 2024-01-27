using Api.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class ApplicationservicesExtensions
    {
        public static IServiceCollection AddApplicatioservices(this IServiceCollection services,IConfiguration config)
        {
             // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
             services.AddEndpointsApiExplorer();
             services.AddSwaggerGen();
             //Register The StoreDBContext
             services.AddDbContext<StoreContext>(opt => {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
             });
             //Register The Product services
             services.AddScoped<IProductRepository, ProductRepository>();
             services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
             //Register The AutoMapper
             services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
             services.Configure<ApiBehaviorOptions>(options =>
             {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var error = actionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErorrResponse
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
            return services;
        }
    }
}
