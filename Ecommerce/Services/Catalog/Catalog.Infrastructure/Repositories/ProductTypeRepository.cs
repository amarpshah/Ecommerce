using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductTypeRepository : ITypesRepository
    {
        private ICatalogContext _catalogContext;

        public ProductTypeRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _catalogContext.Types.Find(p=>true).ToListAsync();
        }
    }
}
