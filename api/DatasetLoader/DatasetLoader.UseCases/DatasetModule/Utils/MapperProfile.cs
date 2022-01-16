using AutoMapper;
using DatasetLoader.UseCases.DatasetModule.Commands.CreateDatasetModule.Dto;
using DatasetLoader.UseCases.DatasetModule.Queries.GetDatasetModules.Dto;

namespace DatasetLoader.UseCases.DatasetModule.Utils
{
    /// <summary>
    /// Настроки AutoMapper'а
    /// </summary>
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Entities.DatasetModule, DatasetModuleDto>();
            CreateMap<CreateDatasetModuleDto, Entities.DatasetModule>();
        }
    }
}