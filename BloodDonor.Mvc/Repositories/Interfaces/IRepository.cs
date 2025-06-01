namespace BloodDonor.Mvc.Repositories.Interfaces
{
    public interface IRepository<T> where T: class 
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        void Add(T bloodDonor);
        void Update(T bloodDonor);
        void Delete(T bloodDonor);
        IQueryable<T> Query();
    }
}
