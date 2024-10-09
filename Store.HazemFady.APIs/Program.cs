
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.HazemFady.APIs.Errors;
using Store.HazemFady.APIs.MiddleWares;
using Store.HazemFady.Core;
using Store.HazemFady.Core.Mapping.Products;
using Store.HazemFady.Core.Services.Contract;
using Store.HazemFady.Repository;
using Store.HazemFady.Repository.Data;
using Store.HazemFady.Repository.Data.Contexts;
using Store.HazemFady.Services.Services.Products;

namespace Store.HazemFady.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(m=>m.AddProfile(new ProductProfile(builder.Configuration)));
            builder.Services.Configure<ApiBehaviorOptions>
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


            var app = builder.Build();

            using var x = app.Services.CreateScope();
            var service = x.ServiceProvider;
            var context = service.GetService<StoreDbContext>();
            var LogFactory = service.GetService<LoggerFactory>();
            try
            {
                await context!.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var Logger = LogFactory!.CreateLogger<Program>();
                Logger.LogError(ex, "There Are Problem When Update And Migrate Database !!!");

            }

             app.UseMiddleware <ExceptionMiddleWare>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
