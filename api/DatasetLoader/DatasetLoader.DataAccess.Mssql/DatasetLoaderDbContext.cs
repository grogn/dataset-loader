using DatasetLoader.DataAccess.Abstractions;
using DatasetLoader.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatasetLoader.DataAccess.Mssql
{
    public class DatasetLoaderDbContext: DbContext, IDatasetLoaderDbContext
    {
        public DbSet<DatasetModule> DatasetModules { get; set; }
        
        public DatasetLoaderDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}