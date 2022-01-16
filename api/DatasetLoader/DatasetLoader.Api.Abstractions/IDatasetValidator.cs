using System.Collections.Generic;
using System.Threading.Tasks;
using DatasetLoader.Api.Dto;

namespace DatasetLoader.Api.Abstractions
{
    public interface IDatasetValidator
    {
        Task<List<ValidationError>> Validate(UploadDatasetModuleDto datasetModuleDto);
    }
}