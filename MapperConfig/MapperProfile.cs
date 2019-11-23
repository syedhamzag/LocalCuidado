﻿using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BaseRecordItem;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.Company;
using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using AwesomeCare.Model.Models;
using System.Linq;

namespace MapperConfig
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
                .ForMember(dto => dto.BaseRecordItems, mem => mem.MapFrom(src => src.BaseRecordItems))
                .ForMember(dto => dto.BaseRecordId, mem => mem.Ignore());

            CreateMap<BaseRecordItemDto, BaseRecordItemModel>()
                .ForMember(dto => dto.ValueName, mem => mem.MapFrom(src => src.ValueName))
                .ForMember(dto => dto.BaseRecordItemId, mem => mem.Ignore())
                .ForMember(dto => dto.BaseRecordId, mem => mem.Ignore())
                .ForMember(dto => dto.Deleted, mem => mem.Ignore())
                .ForMember(dto => dto.BaseRecord, mem => mem.Ignore());

            CreateMap<BaseRecordModel, PostBaseRecord>()
                .ForMember(dt => dt.KeyName, mem => mem.MapFrom(src => src.KeyName))
                .ForMember(dt => dt.Description, mem => mem.MapFrom(src => src.Description))
                .ForMember(dt => dt.BaseRecordItems, mem => mem.MapFrom(src => src.BaseRecordItems));

            CreateMap<BaseRecordModel, GetBaseRecordWithItems>()
                .ForMember(dto => dto.BaseRecordId, mem => mem.MapFrom(src => src.BaseRecordId))
                .ForMember(dto => dto.KeyName, mem => mem.MapFrom(src => src.KeyName))
                .ForMember(dto => dto.Description, mem => mem.MapFrom(src => src.Description))
                .ForMember(dto => dto.BaseRecordItems, mem => mem.MapFrom(src => src.BaseRecordItems));

            CreateMap<PostBaseRecordItem, BaseRecordItemModel>()
                .ForMember(dest => dest.BaseRecordId, mem => mem.MapFrom(src => src.BaseRecordId))
                .ForMember(dest => dest.BaseRecord, mem => mem.Ignore())
                .ForMember(dest => dest.BaseRecordItemId, mem => mem.Ignore())
                .ForMember(dest => dest.Deleted, mem => mem.MapFrom(src => false))
                .ForMember(dest => dest.ValueName, mem => mem.MapFrom(src => src.ValueName));

            CreateMap<BaseRecordItemModel, GetBaseRecordItem>()
                .ForMember(dt => dt.BaseRecordItemId, mem => mem.MapFrom(src => src.BaseRecordItemId))
                .ForMember(dt => dt.BaseRecordId, mem => mem.MapFrom(src => src.BaseRecordId))
                .ForMember(dt => dt.Deleted, mem => mem.MapFrom(src => src.Deleted))
                .ForMember(dt => dt.ValueName, mem => mem.MapFrom(src => src.ValueName))
                .ForMember(dt => dt.KeyName, mem => mem.MapFrom(src => src.BaseRecord.KeyName));

            CreateMap<BaseRecordModel, GetBaseRecord>();

            CreateMap<PutBaseRecordItem, BaseRecordItemModel>()
                .ForMember(dto => dto.BaseRecordId, mem => mem.MapFrom(src => src.BaseRecordId))
                .ForMember(dto => dto.BaseRecordItemId, mem => mem.MapFrom(src => src.BaseRecordItemId))
                .ForMember(dto => dto.Deleted, mem => mem.MapFrom(src => src.Deleted))
                .ForMember(dto => dto.ValueName, mem => mem.MapFrom(src => src.ValueName))
                .ForMember(dto => dto.BaseRecord, mem => mem.Ignore());
            #endregion

            #region BaseRecordItem
            CreateMap<GetBaseRecordItem, PutBaseRecordItem>();
            #endregion

            #region Client
            CreateMap<PostClient, Client>()
                .ForMember(dto => dto.ClientId, mem => mem.Ignore())
                .ForMember(dto => dto.InvolvingParties, mem => mem.Ignore())
                .ForMember(dto => dto.RegulatoryContact, mem => mem.Ignore());

            CreateMap<Client, GetClient>()
                .ForMember(dto => dto.Status, mem => mem.Ignore());
            // .ForMember(dto=>dto.)
            #endregion

            #region ClientInvolvingPartyItem
            CreateMap<PostClientInvolvingPartyItem, ClientInvolvingPartyItem>()
                .ForMember(dto => dto.Deleted, mem => mem.MapFrom(src => false))
                .ForMember(dto => dto.ClientInvolvingPartyItemId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientInvolvingParty, mem => mem.Ignore());
            CreateMap<ClientInvolvingPartyItem, GetClientInvolvingPartyItem>();
            CreateMap<PutClientInvolvingPartyItem, ClientInvolvingPartyItem>()
                .ForMember(dto => dto.ClientInvolvingParty, mem => mem.Ignore());
            #endregion

            #region ClientInvolvingParty
            CreateMap<PostClientInvolvingParty, ClientInvolvingParty>()
                .ForMember(dto => dto.ClientInvolvingPartyId, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.ClientInvolvingPartyItem, mem => mem.Ignore());

            CreateMap<ClientInvolvingParty, GetClientInvolvingParty>()
                .ForMember(dto => dto.ClientInvolvingPartyItemName, mem => mem.MapFrom(src => src.ClientInvolvingPartyItem.ItemName));

            CreateMap<PutClientInvolvingParty, ClientInvolvingParty>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.ClientInvolvingPartyItem, mem => mem.Ignore());
            #endregion

            #region ClientRegulatoryContact

            CreateMap<PostClientRegulatoryContact, ClientRegulatoryContact>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRegulatoryContactId, mem => mem.Ignore())
                .ForMember(dto => dto.BaseRecordItem, mem => mem.Ignore());

            CreateMap<ClientRegulatoryContact, GetClientRegulatoryContact>();
            #endregion

            #region ClientRota
            CreateMap<PostClientRota, Rota>()
                .ForMember(dto => dto.RotaId, mem => mem.Ignore());

            CreateMap<Rota, GetClientRota>();
            CreateMap<PutClientRota, Rota>();
            #endregion
        }
    }
}
