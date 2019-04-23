﻿using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.Company;
using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using AwesomeCare.Model.Models;

namespace AwesomeCare.API.AutoMapperConfig
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Company
            CreateMap<CompanyModel, CreateCompanyDto>();

            CreateMap<CreateCompanyDto, CompanyModel>()
                .ForMember(dto => dto.CompanyId, mem => mem.Ignore())
                .ForMember(dto => dto.CompanyContacts, mem => mem.Ignore());
            #endregion

            #region CompanyContact
            CreateMap<PostCompanyContactDto, CompanyContactModel>()
               .ForMember(dto => dto.CompanyId, mem => mem.Ignore())
               .ForMember(dto => dto.CompanyContactId, mem => mem.Ignore())
               .ForMember(dto => dto.Company, mem => mem.Ignore());

            CreateMap<CompanyContactModel, GetCompanyContactDto>();
            #endregion
        }
    }
}
