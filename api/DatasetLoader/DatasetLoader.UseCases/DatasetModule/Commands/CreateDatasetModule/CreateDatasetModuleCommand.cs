using DatasetLoader.UseCases.DatasetModule.Commands.CreateDatasetModule.Dto;
using MediatR;

namespace DatasetLoader.UseCases.DatasetModule.Commands.CreateDatasetModule
{
    public class CreateDatasetModuleCommand: IRequest<CreateDatasetModuleResponseDto>
    {
        public CreateDatasetModuleDto Dto { get; set; }
    }
}