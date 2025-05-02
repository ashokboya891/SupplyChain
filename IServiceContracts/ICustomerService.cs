using SupplyChain.DTOs;
using SupplyChain.Models;

namespace SupplyChain.IServiceContracts
{
    // Services/ICustomerService.cs
    public interface ICustomerService
    {
        Task<PaginatedList<CustomerDto>> GetAllAsync(int pageIndex, int pageSize);
        Task<CustomerDto> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CreateCustomerDto dto);
        Task UpdateAsync(int id, UpdateCustomerDto dto);
        Task DeleteAsync(int id);
    }
}
