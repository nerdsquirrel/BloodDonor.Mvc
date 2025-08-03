using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonor.Mvc.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        protected readonly DbSet<T> _dbSet;

        public Repository(BloodDonorDbContext context)
        {            
            _dbSet = context.Set<T>();
        }

        public void Add(T bloodDonor)
        {
            _dbSet.Add(bloodDonor);
        }

        public void Delete(T bloodDonor)
        {
            _dbSet.Remove(bloodDonor);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }

        public void Update(T bloodDonor)
        {
            _dbSet.Update(bloodDonor);
        }
    }
}
