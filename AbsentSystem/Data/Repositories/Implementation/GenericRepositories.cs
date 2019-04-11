using AbsentSystem.Data.Entities;
using AbsentSystem.Data.Repositories.Contract;
using AbsentSystem.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AbsentSystem.Data.Repositories.Implementation
{
    public class GenericRepositories<TEntity> : IGenericRepositories<TEntity> where TEntity : class, IEntiity
    {
        protected readonly ApplicationDbContext context;
        public DbSet<TEntity> Entities { get; set; }
        public IQueryable<TEntity> NoTrackEntities => Entities.AsNoTracking();
        public IQueryable<TEntity> TrackEntities => Entities;

        public GenericRepositories(ApplicationDbContext Context)
        {
            context = Context;
            Entities = context.Set<TEntity>();
        }

        public virtual Task<TEntity> GetByIdAsync(object ID, CancellationToken CancellationToken) => Entities.FindAsync(ID, CancellationToken);

        public virtual async Task AddAsync(TEntity entity,CancellationToken cancellationToken)
        {
            if (Assert.NotNull(entity))
            {
                await Entities.AddAsync(entity,cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
        public virtual async Task UpdateAsync(TEntity entity,CancellationToken cancellationToken)
        {
            if (Assert.NotNull(entity))
            {
                Entities.Update(entity);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
        public virtual async Task DeleteAsync(TEntity entity,CancellationToken cancellationToken)
        {
            if (Assert.NotNull(entity))
            {
                Entities.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
