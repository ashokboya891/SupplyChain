using AutoMapper;
using SupplyChain.DTOs;
using SupplyChain.Models;

namespace SupplyChain.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<UpdateCustomerDto, Customer>();
            CreateMap<Customer, UpdateCustomerDto>();


            // Product Mappings
            //CreateMap<Product, ProductDto>()
            //    .ForMember(dest => dest.PriceWithTax,
            //        opt => opt.MapFrom(src => src.Price * 1.2m))
            //    .ReverseMap();

            // Order Mappings
            //CreateMap<Order, OrderDto>()
            //    .ForMember(dest => dest.OrderDate,
            //        opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd")))
            //    .ReverseMap();

            // Add all other mappings here
        }
    }
}
