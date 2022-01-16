using System;

namespace DatasetLoader.UseCases.DatasetModule.Queries.GetDatasetModules.Dto
{
    /// <summary>
    /// Модуль набора данных.
    /// </summary>
    public class DatasetModuleDto
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime Date { get; set; }
    }
}