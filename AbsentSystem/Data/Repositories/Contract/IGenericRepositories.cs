using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AbsentSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbsentSystem.Data.Repositories.Contract
{
    public interface IGenericRepositories<TEntity> where TEntity : class, IEntiity
    {
        DbSet<TEntity> Entities { get; set; }
        IQueryable<TEntity> NoTrackEntities { get; }
        IQueryable<TEntity> TrackEntities { get; }
        
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(object ID, CancellationToken CancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}