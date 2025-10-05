using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SubscriptionManagement.DAL.AppDBContext;
using SubscriptionManagement.DAL.Infrasructure.Interfaces;
using System.Linq.Expressions;

namespace SubscriptionManagement.DAL.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        private readonly ILogger<Repository<T>>? _logger;

        public Repository(AppDbContext context, ILogger<Repository<T>>? logger = null)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("ID cannot be an empty GUID.", nameof(id));

                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error retrieving entity {EntityType} with ID {Id}", typeof(T).Name, id);
                throw;
            }
        }
        public async Task<T?> GetByIdAsync(Guid id, ISpecification<T> spec)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("ID cannot be an empty GUID.", nameof(id));


                IQueryable<T> query = _dbSet.AsQueryable();
                query = SpecificationEvaluator<T>.GetQuery(query, spec);
                if (spec?.Criteria == null)
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, "Id");
                    var equals = Expression.Equal(property, Expression.Constant(id));
                    var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                    query = query.Where(lambda);
                }
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error retrieving entity {EntityType} with ID {Id}", typeof(T).Name, id);
                throw;
            }
        }


        public async Task<IEnumerable<T>> ListAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error listing all entities of type {EntityType}", typeof(T).Name);
                throw;
            }
        }

        public async Task<IEnumerable<T>> ListAsync(ISpecification<T> spec)
        {
            try
            {
                if (spec == null)
                    throw new ArgumentNullException(nameof(spec));

                return await ApplySpecification(spec).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error listing entities of type {EntityType} with specification {Specification}", typeof(T).Name, spec);
                throw;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                await _dbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error adding entity of type {EntityType}", typeof(T).Name);
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error updating entity of type {EntityType}", typeof(T).Name);
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error deleting entity of type {EntityType}", typeof(T).Name);
                throw;
            }
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            try
            {
                return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error applying specification {Specification} to entity type {EntityType}", spec, typeof(T).Name);
                throw;
            }
        }
    }
}
