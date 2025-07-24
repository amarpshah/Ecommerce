using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductBrand> Brands { get; }

        public IMongoCollection<ProductType> Types { get; }

        public CatalogContext(IConfiguration configuration)
        {
            //This requires Configuration.Binder nuget package to build
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Brands = database.GetCollection<ProductBrand>(configuration.GetValue<string>("DatabaseSettings:BrandCollection"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:ProductCollection"));
            Types = database.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:TypeCollection"));

            BrandContextSeed.SeedData(Brands);
            CatalogContextSeed.SeedData(Products);
            TypeContextSeed.SeedData(Types);

        }
    }
}
