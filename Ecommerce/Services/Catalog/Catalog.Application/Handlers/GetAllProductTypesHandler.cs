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
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IList<ProductTypeResponse>>
    {
        private readonly ITypesRepository _typesRepository;
        private readonly IMapper _mapper;

        public GetAllProductTypesHandler(ITypesRepository typesRepository, IMapper mapper)
        {
            this._typesRepository = typesRepository;
            this._mapper = mapper;
        }
        public async Task<IList<ProductTypeResponse>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var prodTypes = await _typesRepository.GetAllTypes();
            var lstTypes = _mapper.Map<List<ProductType>, List<ProductTypeResponse>>(prodTypes.ToList());
            return lstTypes;
        }
    }
}
