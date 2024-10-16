using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Entities.Order;
using Store.HazemFady.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.HazemFady.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext store)
        {
            if (store.Brands.Count() == 0)
            {
                //1. Read Data From File Json    
                var BrandData = File.ReadAllText(@"..\Store.HazemFady.Repository\Data\DataSeed\brands.json");
                //2. Convert Json String To List<T>   

                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                //3. Seed Data To DataBase
                if (Brands is not null && Brands.Count > 0)
                {
                    await store.Brands.AddRangeAsync(Brands);
                    await store.SaveChangesAsync();

                }
            }


            if (store.Types.Count() == 0)
            {
                //1. Read Data From File Json    
                var TypesData = File.ReadAllText(@"..\Store.HazemFady.Repository\Data\DataSeed\types.json");
                //2. Convert Json String To List<T>   

                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                //3. Seed Data To DataBase
                if (Types is not null && Types.Count > 0)
                {
                    await store.Types.AddRangeAsync(Types);
                    await store.SaveChangesAsync();

                }
            }


            if (store.Products.Count() == 0)
            {
                //1. Read Data From File Json    
                var productData = File.ReadAllText(@"..\Store.HazemFady.Repository\Data\DataSeed\products.json");
                //2. Convert Json String To List<T>   

                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                //3. Seed Data To DataBase
                if (products is not null && products.Count > 0)
                {
                    await store.Products.AddRangeAsync((products));
                    await store.SaveChangesAsync();

                }
            }

            if (store.DeliveryMethods.Count() == 0)
            {
                //1. Read Data From File Json    
                var deliveryData = File.ReadAllText(@"..\Store.HazemFady.Repository\Data\DataSeed\delivery.json");
                //2. Convert Json String To List<T>   

                var Deliveryies = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                //3. Seed Data To DataBase
                if (Deliveryies is not null && Deliveryies.Count > 0)
                {
                    await store.DeliveryMethods.AddRangeAsync((Deliveryies));
                    await store.SaveChangesAsync();

                }
            }
        }
    }
}
