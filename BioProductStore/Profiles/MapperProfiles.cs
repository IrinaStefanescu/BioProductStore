using BioProductStore.DTOs;
using BioProductStore.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            //User
            CreateMap<User, UserResponseDTO>();
            //CreateMap<User, UserRegisterDTO>(); 
            CreateMap<RegisterUserDTO, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<User, LoginUserDTO>();
            CreateMap<User, UserResponseTokenDTO>();

            //Category
            CreateMap<RegisterCategoryDTO, Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<Category, CategoryResponseDTO>();

            //Product
            CreateMap<RegisterProductDTO, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<UpdateProductDTO, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null && src.Price != 0)); ;
            CreateMap<Product, ProductResponseDTO>();

            //Order
            CreateMap<RegisterOrderDTO, Order>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<UpdateOrderDTO, Order>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;

            //DeliveryAddress
            CreateMap<RegisterExpeditionAddressDTO, ExpeditionAddress>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<UpdateExpeditionAddressDTO, ExpeditionAddress>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;

            //OrderProductRelation
            CreateMap<OrderProductRegisterDTO, OrderProduct>();
        }
    }
}
