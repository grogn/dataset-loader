using System.Threading;
using System.Threading.Tasks;
using DatasetLoader.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatasetLoader.DataAccess.Abstractions
{
    public interface IDatasetLoaderDbContext
    {
        /// <summary>
        /// Проекты.
        /// </summary>
        public DbSet<DatasetModule> DatasetModules { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}