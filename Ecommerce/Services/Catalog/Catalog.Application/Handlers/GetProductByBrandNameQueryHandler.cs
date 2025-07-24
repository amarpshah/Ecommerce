using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetProductByBrandNameQueryHandler : IRequestHandler<GetProductByBrandNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetProductByBrandNameQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByBrandNameQuery request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetProductsByBrand(request.Brand);
            var lst = mapper.Map<List<Product>, List<ProductResponse>>(result.ToList());
            return lst;
        }
    }
}
