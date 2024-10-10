// Ignore Spelling: app

using Microsoft.AspNetCore.Builder;
using Store.HazemFady.APIs.MiddleWares;
using Store.HazemFady.Repository.Data.Contexts;
using Store.HazemFady.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Store.HazemFady.APIs.SharedProgram
{
    public static  class ConfigureMiddleWare
    {

        public static async Task<WebApplication> ConfigureMiddleWareAfterBuildInProgram(this WebApplication app)
        {
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

            app.UseMiddleware<ExceptionMiddleWare>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

         

            return app;
        }
    }
}
