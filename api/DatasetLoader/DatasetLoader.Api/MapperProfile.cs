using AutoMapper;
using DatasetLoader.Api.Dto;
using DatasetLoader.UseCases.DatasetModule.Commands.CreateDatasetModule.Dto;

namespace DatasetLoader.Api
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<UploadDatasetModuleDto, CreateDatasetModuleDto>();
        }
    }
}