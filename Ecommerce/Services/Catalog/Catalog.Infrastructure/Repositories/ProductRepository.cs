using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext) 
        {
            _catalogContext = catalogContext; 
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
            return product;
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _catalogContext.Products.ReplaceOneAsync( p=> p.Id == product.Id, product);
            return (result.IsAcknowledged == true && result.ModifiedCount > 0 );
        }
        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = _catalogContext.Products.DeleteOneAsync(p => p.Id == id);
            return (deleteResult.Result.IsAcknowledged == true && deleteResult.Result.DeletedCount > 0);
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p=> p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pagination<Product>> GetProducts(CatalogSpecParams catalogSpecParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
                filter &= builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));

            if (catalogSpecParams.BrandId != null)
                filter &= builder.Eq(p => p.Brand.Id, catalogSpecParams.BrandId);

            if (catalogSpecParams.TypeId != null)
                filter &= builder.Eq(p => p.Types.Id, catalogSpecParams.TypeId);

            int skipCnt = (catalogSpecParams.PageIndex-1 > 0? catalogSpecParams.PageIndex -1 : 0 ) * catalogSpecParams.PageSize;
            var totalItems = await _catalogContext.Products.CountDocumentsAsync(filter);
            var data = await _catalogContext.Products.Find(filter)
                                                    .Skip(skipCnt)
                                                    .Limit(catalogSpecParams.PageSize)
                                                    .ToListAsync();


            return new Pagination<Product> (catalogSpecParams.PageIndex, catalogSpecParams.PageSize, (int) totalItems, data);
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string name)
        {
            return await _catalogContext.Products.Find(p => p.Brand.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _catalogContext.Products.Find(p => p.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByType(string name)
        {
            return await _catalogContext.Products.Find(p => p.Types.Name == name).ToListAsync();
        }

    }
}
