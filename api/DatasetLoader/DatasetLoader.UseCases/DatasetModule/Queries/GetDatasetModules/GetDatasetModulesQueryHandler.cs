using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DatasetLoader.DataAccess.Abstractions;
using DatasetLoader.UseCases.DatasetModule.Queries.GetDatasetModules.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatasetLoader.UseCases.DatasetModule.Queries.GetDatasetModules
{
    public class GetDatasetModulesQueryHandler: IRequestHandler<GetDatasetModulesQuery, List<DatasetModuleDto>>
    {
        private readonly IDatasetLoaderDbContext _dbContext;

        private readonly IMapper _mapper;
        
        public GetDatasetModulesQueryHandler(IDatasetLoaderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<List<DatasetModuleDto>> Handle(GetDatasetModulesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.DatasetModules
                .Select(x => _mapper.Map<DatasetModuleDto>(x))
                .ToListAsync(cancellationToken);
        }
    }
}