using AutoMapper;

using MiniEcommerceWebApi.Core.DTO.Customer;
using MiniEcommerceWebApi.Core.DTO.Order;
using MiniEcommerceWebApi.Core.DTO.OrderItem;
using MiniEcommerceWebApi.Core.DTO.Product;
using MiniEcommerceWebApi.Core.Model;

namespace MiniEcommerceWebApi.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            #region Login
            CreateMap<LoginDTO, Customer>(MemberList.None).ReverseMap();
            CreateMap<CustomerDTO, Customer>(MemberList.None).ReverseMap();

            #endregion

            #region Order
            CreateMap<CreateOrderDTO, Order>(MemberList.None).ReverseMap();
            CreateMap<OrderDTO, Order>(MemberList.None).ReverseMap();

            #endregion

            #region LoProductgin
            CreateMap<CreateProductDTO, Product>(MemberList.None).ReverseMap();
            CreateMap<ProductDTO, Product>(MemberList.None).ReverseMap();
            CreateMap<UpdateProductDTO, ProductDTO>(MemberList.None).ReverseMap();
            #endregion

            #region OrderItem
            CreateMap<OrderItemDTO, OrderItem>(MemberList.None).ReverseMap();
            CreateMap<CreateOrderItemDTO, OrderItem>(MemberList.None).ReverseMap();
           
            #endregion
        }
    }
}
