using Microsoft.AspNetCore.Identity;
using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Entities.Identity;
using Store.HazemFady.Repository.Data.Contexts;
using Store.HazemFady.Repository.Identity.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.HazemFady.Repository.Identity
{
    public static class StoreIdentityDbContextSeed
    {
        public static async Task SeedAPPUserAsync(UserManager<APPUser> userManager)
        {
            if (userManager.Users.Count() == 0)
            {

            var User=new APPUser()
            {
                Email="hazemfady123@gmail.com",
                PhoneNumber="01110878937",
                DisplayName="Hazem Fady",
                UserName="Hazem.Fady",
                Address=new Address()
                {
                    FName="Hazem",
                    LName="Fady",
                    Country="Egypt",
                    City="Cairo",
                    Street="Villel"
                }

            };
           await  userManager.CreateAsync(User,"Hazem@fady135790");
            }
            

        }

        //public static async Task SeedAsync(StoreDbContext store)
        //{
        //    if (store.Brands.Count() == 0)
        //    {
        //        //1. Read Data From File Json    
        //        var BrandData = File.ReadAllText(@"..\Store.HazemFady.Repository\Data\DataSeed\brands.json");
        //        //2. Convert Json String To List<T>   

        //        var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
        //        //3. Seed Data To DataBase
        //        if (Brands is not null && Brands.Count > 0)
        //        {
        //            await store.Brands.AddRangeAsync(Brands);
        //            await store.SaveChangesAsync();

        //        }
        //    }


        //    if (store.Types.Count() == 0)
        //    {
        //        //1. Read Data From File Json    
        //        var TypesData = File.ReadAllText(@"..\Store.HazemFady.Repository\Data\DataSeed\types.json");
        //        //2. Convert Json String To List<T>   

        //        var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
        //        //3. Seed Data To DataBase
        //        if (Types is not null && Types.Count > 0)
        //        {
        //            await store.Types.AddRangeAsync(Types);
        //            await store.SaveChangesAsync();

        //        }
        //    }


        //    if (store.Products.Count() == 0)
        //    {
        //        //1. Read Data From File Json    
        //        var productData = File.ReadAllText(@"..\Store.HazemFady.Repository\Data\DataSeed\products.json");
        //        //2. Convert Json String To List<T>   

        //        var products = JsonSerializer.Deserialize<List<Product>>(productData);
        //        //3. Seed Data To DataBase
        //        if (products is not null && products.Count > 0)
        //        {
        //            await store.Products.AddRangeAsync((products));
        //            await store.SaveChangesAsync();

        //        }
        //    }
        //}




    }
}
