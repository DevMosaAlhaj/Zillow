using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;
using Zillow.Data.DbEntity;

namespace Zillow.Service.Extensions
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // DbEntity to ViewModel 

            CreateMap<AddressDbEntity, AddressViewModel>();
            CreateMap<CategoryDbEntity, CategoryViewModel>();
            CreateMap<ContractDbEntity, ContractViewModel>()
                .ForMember(x=> x.ContractType,
                    x=> 
                        x.MapFrom(y=> y.ContractType.ToString()));
            
            CreateMap<CustomerDbEntity, CustomerViewModel>();
            CreateMap<ImageDbEntity, ImageViewModel>();
            CreateMap<RealEstateDbEntity, RealEstateViewModel>();
            CreateMap<UserDbEntity, UserViewModel>()
                .ForMember(x=> x.UserType,x=> 
                    x.MapFrom(i=> i.UserType.ToString()));

            // CreateDto to DbEntity

            CreateMap<CreateAddressDto, AddressDbEntity>();
            CreateMap<CreateCategoryDto, CategoryDbEntity>();
            CreateMap<CreateContractDto, ContractDbEntity>();
            CreateMap<CreateCustomerDto, CustomerDbEntity>();
            CreateMap<CreateImageDto, ImageDbEntity>();
            CreateMap<CreateRealEstatesDto, RealEstateDbEntity>();
            CreateMap<CreateUserDto, UserDbEntity>();

            // UpdateDto to DbEntity
            
            CreateMap<UpdateAddressDto, AddressDbEntity>()
                .ForAllMembers(otp=>
                    otp.Condition(((src, destination, srcMember) => srcMember!= null )));
            CreateMap<UpdateCategoryDto, CategoryDbEntity>()
                .ForAllMembers(otp=>
                    otp.Condition(((src, destination, srcMember) => srcMember!= null )));
            CreateMap<UpdateContractDto, ContractDbEntity>()
                .ForAllMembers(otp=>
                    otp.Condition(((src, destination, srcMember) => srcMember!= null )));
            CreateMap<UpdateCustomerDto, CustomerDbEntity>()
                .ForAllMembers(otp=>
                    otp.Condition(((src, destination, srcMember) => srcMember!= null )));
            CreateMap<UpdateImageDto, ImageDbEntity>()
                .ForAllMembers(otp=>
                    otp.Condition(((src, destination, srcMember) => srcMember!= null )));
            CreateMap<UpdateRealEstatesDto, RealEstateDbEntity>()
                .ForAllMembers(otp=> 
                    otp.Condition((src,destination,srcMember)=> srcMember!=null));
            CreateMap<UpdateUserDto, UserDbEntity>()
                .ForAllMembers(otp=> 
                    otp.Condition((src,destination,srcMember)=> srcMember!=null));

        }
    }
}
