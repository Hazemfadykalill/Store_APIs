// Ignore Spelling: app

using Microsoft.AspNetCore.Builder;
using Store.HazemFady.APIs.MiddleWares;
using Store.HazemFady.Repository.Data.Contexts;
using Store.HazemFady.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Store.HazemFady.Repository.Identity.Contexts;
using Store.HazemFady.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Store.HazemFady.Core.Entities.Identity;

namespace Store.HazemFady.APIs.SharedProgram
{
    public static  class ConfigureMiddleWare
    {

        public static async Task<WebApplication> ConfigureMiddleWareAfterBuildInProgram(this WebApplication app)
        {
            using var x = app.Services.CreateScope();
            var service = x.ServiceProvider;
            var context = service.GetRequiredService<StoreDbContext>();
            var contextIdentity = service.GetRequiredService<StoreIdentityDbContext>();
            var userManger = service.GetRequiredService<UserManager<APPUser>>();
            var LogFactory = service.GetService<LoggerFactory>();
            try
            {
                await context!.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);

                await contextIdentity!.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAPPUserAsync(userManger);
              


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
