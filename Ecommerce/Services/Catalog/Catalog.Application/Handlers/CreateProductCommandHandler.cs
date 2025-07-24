using AutoMapper;
using Catalog.Application.Commands;
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
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper) 
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var prodEntity = _mapper.Map<Product>(request);
            if (prodEntity is null) {
                throw new ApplicationException("Issue with mapping while creating new product");
            }
            var prod = await _productRepository.CreateProduct(prodEntity);
            var prodResp = _mapper.Map<ProductResponse>(prod);
            return prodResp;
        }
    }
}
