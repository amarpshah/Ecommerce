using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        { 
            bool checkProducts = productCollection.Find(b => true).Any();
            string path = Path.Combine("Data", "SeedData", "Product.json");
            if (!checkProducts)
            {
                var productsData = File.ReadAllText(path);
                //var productsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/Product.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            
                if (products != null)
                {
                    foreach(var p in products)
                    {
                        productCollection.InsertOneAsync(p);
                    }
                }
            }
        }
    }
}
