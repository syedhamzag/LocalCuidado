using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.Company;
using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using AwesomeCare.Model.Models;
using System.Linq;

namespace AwesomeCare.API.AutoMapperConfig
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Company
            CreateMap<CompanyModel, GetCompanyDto>()
               .ForMember(dto => dto.Company, mem => mem.MapFrom(src => src.CompanyName));
            //.ForMember(dto=>dto.Contacts,mem=>mem.MapFrom(src=>src.CompanyContacts.AsQueryable()));
            //   .ForMember(dto=>dto.Contacts,mem=>mem.Ignore());

            CreateMap<CreateCompanyDto, CompanyModel>()
                .ForMember(dto => dto.CompanyId, mem => mem.Ignore())
                .ForMember(dto => dto.CompanyName, mem => mem.MapFrom(src => src.Company))
            .ForMember(dto => dto.CompanyContact, mem => mem.Ignore());

            CreateMap<UpdateCompanyDto, CompanyModel>()
               .ForMember(dto => dto.CompanyName, mem => mem.MapFrom(src => src.Company))
            .ForMember(dto => dto.CompanyContact, mem => mem.Ignore());

            CreateMap<CompanyModel, UpdateCompanyDto>()
                .ForMember(dto => dto.Company, mem => mem.MapFrom(src => src.CompanyName));

            CreateMap<GetCompanyDto, UpdateCompanyDto>();
            #endregion

            #region CompanyContact
            CreateMap<PostCompanyContactDto, CompanyContactModel>()
               .ForMember(dto => dto.CompanyContactId, mem => mem.Ignore())
              .ForMember(dto => dto.Company, mem => mem.Ignore());
            
            CreateMap<CompanyContactModel, GetCompanyContactDto>();
            #endregion
        }
    }
}
