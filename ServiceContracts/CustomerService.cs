using AutoMapper;
using SupplyChain.DTOs;
using SupplyChain.Extensions;
using SupplyChain.IServiceContracts;
using SupplyChain.Models;

namespace SupplyChain.ServiceContracts
{
    // Services/CustomerService.cs
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CustomerDto>> GetAllAsync(int pageIndex, int pageSize)
        {
            var customers = await _repository.GetAllAsync(pageIndex, pageSize);
            return _mapper.ToPaginatedList<Customer, CustomerDto>(customers);
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _repository.AddAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(int id, UpdateCustomerDto dto)
        {
            var customer = await _repository.GetByIdAsync(id);
            _mapper.Map(dto, customer);
            await _repository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
