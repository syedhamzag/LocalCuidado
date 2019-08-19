using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.Client;
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

            #region BaseRecord
            CreateMap<PostBaseRecord, BaseRecordModel>()
                .ForMember(dto => dto.KeyName, mem => mem.MapFrom(src => src.KeyName))
                .ForMember(dto => dto.Description, mem => mem.MapFrom(src => src.Description))
                .ForMember(dto => dto.BaseRecordItems, mem => mem.MapFrom(src=>src.PostBaseRecordItems))
                .ForMember(dto => dto.BaseRecordId, mem => mem.Ignore());

            CreateMap<BaseRecordModel, PostBaseRecord>()
                .ForMember(dt => dt.KeyName, mem => mem.MapFrom(src => src.KeyName))
                .ForMember(dt => dt.Description, mem => mem.MapFrom(src => src.Description))
                .ForMember(dt => dt.PostBaseRecordItems, mem => mem.MapFrom(src => src.BaseRecordItems));

            CreateMap<BaseRecordModel, GetBaseRecordWithItems>()
                .ForMember(dto => dto.BaseRecordId, mem => mem.MapFrom(src => src.BaseRecordId))
                .ForMember(dto => dto.KeyName, mem => mem.MapFrom(src => src.KeyName))
                .ForMember(dto => dto.Description, mem => mem.MapFrom(src => src.Description))
                .ForMember(dto => dto.BaseRecordItems, mem => mem.MapFrom(src => src.BaseRecordItems));

            CreateMap<PostBaseRecordItem, BaseRecordItemModel>()
                .ForMember(dest => dest.BaseRecordId, mem => mem.Ignore())
                .ForMember(dest => dest.BaseRecord, mem => mem.Ignore())
                .ForMember(dest => dest.BaseRecordItemId, mem => mem.Ignore())
                .ForMember(dest => dest.Deleted, mem => mem.MapFrom(src => src.Deleted))
                .ForMember(dest => dest.ValueName, mem => mem.MapFrom(src => src.ValueName));

            CreateMap<BaseRecordItemModel, GetBaseRecordItem>()
                .ForMember(dt => dt.BaseRecordItemId, mem => mem.MapFrom(src => src.BaseRecordItemId))
                .ForMember(dt => dt.BaseRecordId, mem => mem.MapFrom(src => src.BaseRecordId))
                .ForMember(dt => dt.Deleted, mem => mem.MapFrom(src => src.Deleted))
                .ForMember(dt => dt.ValueName, mem => mem.MapFrom(src => src.ValueName));

            CreateMap<BaseRecordModel, GetBaseRecord>();

            #endregion

            #region BaseRecordItem

            #endregion

            #region Client
            CreateMap<PostClient, Client>()
                .ForMember(dto=>dto.ClientId,mem=>mem.Ignore());

            CreateMap<Client, GetClient>();
            #endregion
        }
    }
}
