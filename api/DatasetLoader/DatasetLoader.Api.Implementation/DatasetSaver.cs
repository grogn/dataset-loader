using System;
using System.IO;
using DatasetLoader.Api.Abstractions;
using DatasetLoader.Api.Implementation.Configurations;
using Microsoft.Extensions.Options;

namespace DatasetLoader.Api.Implementation
{
    public class DatasetSaver: IDatasetSaver
    {
        private readonly DatasetSaverConfiguration _configuration;
        
        public DatasetSaver(IOptions<DatasetSaverConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }
        
        public string Save(Stream dataset)
        {
            Directory.CreateDirectory(_configuration.ModulesFolder);
            var datasetPath = Path.Combine(_configuration.ModulesFolder, $"{Guid.NewGuid()}.zip");
            using var fileStream = File.Create(datasetPath);
            dataset.Seek(0, SeekOrigin.Begin);
            dataset.CopyTo(fileStream);
            return Path.GetFullPath(datasetPath);
        }
    }
}