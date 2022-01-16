using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DatasetLoader.DataAccess.Abstractions;
using DatasetLoader.UseCases.DatasetModule.Commands.CreateDatasetModule.Dto;
using MediatR;

namespace DatasetLoader.UseCases.DatasetModule.Commands.CreateDatasetModule
{
    public class CreateDatasetModuleCommandHandler: IRequestHandler<CreateDatasetModuleCommand, CreateDatasetModuleResponseDto>
    {
        private readonly IDatasetLoaderDbContext _dbContext;

        private readonly IMapper _mapper;
        
        public CreateDatasetModuleCommandHandler(IDatasetLoaderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<CreateDatasetModuleResponseDto> Handle(CreateDatasetModuleCommand request, CancellationToken cancellationToken)
        {
            var datasetModule = _mapper.Map<CreateDatasetModuleDto, Entities.DatasetModule>(request.Dto);
            await _dbContext.DatasetModules.AddAsync(datasetModule, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new CreateDatasetModuleResponseDto();
        }
    }
}