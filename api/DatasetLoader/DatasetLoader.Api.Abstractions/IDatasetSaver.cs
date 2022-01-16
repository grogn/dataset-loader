using System.IO;

namespace DatasetLoader.Api.Abstractions
{
    public interface IDatasetSaver
    {
        string Save(Stream dataset);
    }
}