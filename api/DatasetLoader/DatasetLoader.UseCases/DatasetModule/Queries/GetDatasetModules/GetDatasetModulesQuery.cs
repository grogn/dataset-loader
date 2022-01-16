using System.Collections.Generic;
using DatasetLoader.UseCases.DatasetModule.Queries.GetDatasetModules.Dto;
using MediatR;

namespace DatasetLoader.UseCases.DatasetModule.Queries.GetDatasetModules
{
    /// <summary>
    /// Запрос списка загруженных модулей.
    /// </summary>
    public class GetDatasetModulesQuery: IRequest<List<DatasetModuleDto>>
    {
        
    }
}