﻿using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        { 
            bool checkBrands = brandCollection.Find(b => true).Any();
            string path = Path.Combine("Data", "SeedData", "ProductBrand.json");

            if (!Path.Exists(path))
            { 
                path = Path.GetFullPath("../Catalog.Infrastructure/Data/SeedData/ProductBrand.json");
            }

            if (!checkBrands)
            {
                var brandsData = File.ReadAllText(path);
                //var brandsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/ProductBrand.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            
                if (brands!=null)
                {
                    foreach(var brand in brands)
                    {
                        brandCollection.InsertOneAsync(brand);
                    }
                }
            }
        }
    }
}
