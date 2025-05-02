using SupplyChain.Models;

namespace SupplyChain.IServiceContracts
{
    // Repositories/ICustomerRepository.cs
    public interface ICustomerRepository
    {
        Task<PaginatedList<Customer>> GetAllAsync(int pageIndex, int pageSize);
        Task<Customer> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
    }
}
