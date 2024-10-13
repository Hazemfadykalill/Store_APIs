using Microsoft.EntityFrameworkCore;
using Store.HazemFady.Core.Services.Contract;
using Store.HazemFady.Core;
using Store.HazemFady.Repository;
using Store.HazemFady.Repository.Data.Contexts;
using Store.HazemFady.Services.Services.Products;
using Store.HazemFady.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using Store.HazemFady.APIs.Errors;
using Store.HazemFady.Core.Mapping.Baskets;
using Store.HazemFady.Core.Repositories.Contract;
using Store.HazemFady.Repository.Repositories;
using StackExchange.Redis;
using Store.HazemFady.APIs.Attributes;
using Store.HazemFady.Services.Services.Caches;
using Store.HazemFady.Repository.Identity.Contexts;
using Store.HazemFady.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Store.HazemFady.Services.Services.Tokens;
using Store.HazemFady.Services.Services.Users;

namespace Store.HazemFady.APIs.SharedProgram
{
    public static class DependencyInjections
    {



        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServcies();
            services.AddSwaggerServcies();
            services.AddDbServcies(configuration);
            services.AddUserDefinedServcies();
            services.AddAutoMapperServcies(configuration);
            services.ConfigureInvalidModelStateResponseServcies();
            services.AddRedisServcies(configuration);
            services.IdentityService();
            return services;

        }
        private static IServiceCollection AddBuiltInServcies(this IServiceCollection services)
        {
            services.AddControllers();
            return services;

        }
        private static IServiceCollection AddSwaggerServcies(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;

        }
        private static IServiceCollection AddDbServcies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            return services;

        }
        private static IServiceCollection AddUserDefinedServcies(this IServiceCollection services)
        {

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICashService,CacheService>();
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;

        }
        private static IServiceCollection AddAutoMapperServcies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));
            return services;

        }
        private static IServiceCollection ConfigureInvalidModelStateResponseServcies(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>
    (
    options =>
    {
        options.InvalidModelStateResponseFactory = (actionContext) =>
        {
            var Errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count() > 0)
                                               .SelectMany(P => P.Value!.Errors)
                                               .Select(E => E.ErrorMessage)
                                               .ToArray();

            var Response = new APIValidationErrorResponse()
            {
                Errors = Errors
            };
            return new BadRequestObjectResult(Response);
        };

    }

    );
            return services;

        }


        private static IServiceCollection AddRedisServcies(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connect = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connect);
            });
            return services;

        }


        private static IServiceCollection IdentityService(this IServiceCollection services)
        {
            services.AddIdentity<APPUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>(); 

            return services;

        }
    }
}
