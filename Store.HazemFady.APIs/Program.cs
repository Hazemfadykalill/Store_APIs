
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.HazemFady.APIs.Errors;
using Store.HazemFady.APIs.MiddleWares;
using Store.HazemFady.APIs.SharedProgram;
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

            builder.Services.AddDependency(builder.Configuration);

            var app = builder.Build();

            // Add Any middleWare to the container.
            await app.ConfigureMiddleWareAfterBuildInProgram();
            app.Run();


        }
    }
}
