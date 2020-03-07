using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BaseRecordItem;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetails;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsTask;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaDays;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaTask;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using AwesomeCare.DataTransferObject.DTOs.Company;
using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using AwesomeCare.DataTransferObject.DTOs.RotaTask;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffCommunication;
using AwesomeCare.DataTransferObject.DTOs.Untowards;
using AwesomeCare.DataTransferObject.Enums;
using AwesomeCare.Model.Models;
using System;
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
                .ForMember(dto=>dto.RegulatoryContact,mem=>mem.MapFrom(src=>src.RegulatoryContacts))
                .ForMember(dto=>dto.ClientCareDetails,mem=>mem.MapFrom(src=>src.CareDetails))
                .ForMember(dto=>dto.InvolvingParties,mem=>mem.MapFrom(src=>src.InvolvingParties))
                .ForMember(dto => dto.ClientId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
                .ForMember(dto => dto.UniqueId, mem => mem.Ignore());

            CreateMap<Client, GetClient>()
                .ForMember(dto => dto.QRCode, mem => mem.Ignore())
                .ForMember(dto => dto.Gender, mem => mem.Ignore())
                .ForMember(dto => dto.Status, mem => mem.Ignore());

            CreateMap<Client, GetClientDetail>()
               .ForMember(dto => dto.FullName, mem => mem.MapFrom(src => string.Concat(src.Firstname," ",src.Middlename," ",src.Surname)));
              
            


            CreateMap<Client, GetClientForEdit>()
                .ForMember(dto => dto.ClientImage, mem => mem.Ignore())
                .ForMember(dto => dto.InvolvingParties, mem => mem.MapFrom(src => src.InvolvingParties))
                .ForMember(dto => dto.RegulatoryContact, mem => mem.MapFrom(src => src.RegulatoryContact));
            // .ForMember(dto=>dto.)

            CreateMap<GetClientForEdit, PutClient>();

            CreateMap<PutClient, Client>()
                 .ForMember(dto => dto.ClientId, mem => mem.Ignore())
                .ForMember(dto => dto.InvolvingParties, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
                .ForMember(dto => dto.UniqueId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCareDetails, mem => mem.Ignore())
                .ForMember(dto => dto.RegulatoryContact, mem => mem.Ignore());
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

            CreateMap<ClientInvolvingParty, GetClientInvolvingPartyForEdit>();
            #endregion

            #region ClientRegulatoryContact

            CreateMap<PostClientRegulatoryContact, ClientRegulatoryContact>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRegulatoryContactId, mem => mem.Ignore())
                .ForMember(dto => dto.BaseRecordItem, mem => mem.Ignore());

            CreateMap<ClientRegulatoryContact, GetClientRegulatoryContact>();
            CreateMap<ClientRegulatoryContact, GetClientRegulatoryContactForEdit>();
            #endregion

            #region ClientRotaName
            CreateMap<PostClientRotaName, Rota>()
                .ForMember(dto => dto.RotaId, mem => mem.Ignore());

            CreateMap<Rota, GetClientRotaName>();
            CreateMap<PutClientRotaName, Rota>();
            #endregion

            #region ClientRotaType
            CreateMap<ClientRotaType, GetClientRotaType>();
            CreateMap<PutClientRotaType, ClientRotaType>()
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore());

            CreateMap<PostClientRotaType, ClientRotaType>()
                .ForMember(dto => dto.RotaType, mem => mem.MapFrom(src => src.RotaType.ToUpper()))
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaTypeId, mem => mem.Ignore());
            #endregion

            #region RotaTask
            CreateMap<RotaTask, GetRotaTask>();
            CreateMap<PostRotaTask, RotaTask>()
                .ForMember(dto => dto.ClientRotaTask, mem => mem.Ignore())
                .ForMember(dto => dto.RotaTaskId, mem => mem.Ignore());
            CreateMap<PutRotaTask, RotaTask>()
                .ForMember(dto => dto.ClientRotaTask, mem => mem.Ignore());
            #endregion

            #region RotaDayofWeek
            CreateMap<RotaDayofWeek, GetRotaDayofWeek>();
            CreateMap<PostRotaDayofWeek, RotaDayofWeek>()
                .ForMember(dto => dto.Deleted, mem => mem.MapFrom(src => false))
                .ForMember(dto => dto.ClientRotaDays, mem => mem.Ignore())
                .ForMember(dto => dto.RotaDayofWeekId, mem => mem.Ignore());
            CreateMap<PutRotaDayofWeek, RotaDayofWeek>()
                .ForMember(dto => dto.ClientRotaDays, mem => mem.Ignore());
            #endregion

            #region ClientRota
            CreateMap<ClientRota, GetClientRota>();
            CreateMap<PostClientRota, ClientRota>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaType, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaDays, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaId, mem => mem.Ignore());
            CreateMap<PutClientRota, ClientRota>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaType, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaDays, mem => mem.Ignore());

            CreateMap<CreateClientRota, ClientRota>()
                .ForMember(dto => dto.ClientRotaId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaType, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaDays, mem => mem.MapFrom(src => src.ClientRotaDays));

            #endregion

            #region ClientRotaDays
            CreateMap<ClientRotaDays, GetClientRotaDays>();
            CreateMap<PostClientRotaDays, ClientRotaDays>()
                .ForMember(dto => dto.ClientRotaDaysId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
                .ForMember(dto => dto.RotaDayofWeek, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaTask, mem => mem.Ignore());
            CreateMap<PutClientRotaDays, ClientRotaDays>()
              .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
              .ForMember(dto => dto.RotaDayofWeek, mem => mem.Ignore())
              .ForMember(dto => dto.ClientRotaTask, mem => mem.Ignore());

            CreateMap<CreateClientRotaDays, ClientRotaDays>()
                 .ForMember(dto => dto.ClientRotaDaysId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
                .ForMember(dto => dto.RotaDayofWeek, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaTask, mem => mem.MapFrom(src => src.RotaTasks));
            #endregion

            #region ClientRotaTask
            CreateMap<ClientRotaTask, GetClientRotaTask>();
            CreateMap<PostClientRotaTask, ClientRotaTask>()
                .ForMember(dto => dto.ClientRotaTaskId, mem => mem.Ignore())
                .ForMember(dto => dto.RotaTask, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaDays, mem => mem.Ignore());

            CreateMap<PutClientRotaTask, ClientRotaTask>()
                .ForMember(dto => dto.RotaTask, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaDays, mem => mem.Ignore());

            CreateMap<CreateClientRotaTask, ClientRotaTask>()
               .ForMember(dto => dto.ClientRotaTaskId, mem => mem.Ignore())
               .ForMember(dto => dto.RotaTask, mem => mem.Ignore())
               .ForMember(dto => dto.ClientRotaDays, mem => mem.Ignore());
            #endregion

            #region ClientCareDetailsHeading
            CreateMap<ClientCareDetailsHeading, GetClientCareDetailsHeading>();

            CreateMap<PostClientCareDetailsHeading, ClientCareDetailsHeading>()
                .ForMember(dto => dto.ClientCareDetailsHeadingId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCareDetailsTasks, mem => mem.Ignore());

            CreateMap<PutClientCareDetailsHeading, ClientCareDetailsHeading>()
              .ForMember(dto => dto.ClientCareDetailsTasks, mem => mem.Ignore());

            CreateMap<PostClientCareDetailsHeadingTask, ClientCareDetailsTask>()
                .ForMember(dto => dto.ClientCareDetailsHeading, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCareDetails, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCareDetailsHeadingId, mem => mem.Ignore())
                .ForMember(dto => dto.Deleted, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCareDetailsTaskId, mem => mem.Ignore());

            //CreateMap<PostClientCareDetailsHeadingTask, ClientCareDetailsTask>()
            //    .ForMember(dto => dto.ClientCareDetailsHeading, mem => mem.Ignore())
            //    .ForMember(dto => dto.ClientCareDetailsHeadingId, mem => mem.Ignore())
            //    .ForMember(dto => dto.ClientCareDetailsTaskId, mem => mem.Ignore());

            CreateMap<PostClientCareDetailsHeadingWithTasks, ClientCareDetailsHeading>()
                .ForMember(dto => dto.ClientCareDetailsHeadingId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCareDetailsTasks, mem => mem.MapFrom(src => src.Tasks))
                .ForMember(dto => dto.Deleted, mem => mem.MapFrom(src => false));

            CreateMap<ClientCareDetailsHeading, GetClientCareDetailsHeadingWithTasks>()
                .ForMember(dto => dto.Tasks, mem => mem.MapFrom(src => src.ClientCareDetailsTasks.Where(t=>!t.Deleted)));
            #endregion

            #region ClientCareDetailsTask
            CreateMap<ClientCareDetailsTask, GetClientCareDetailsTask>();

            CreateMap<PostClientCareDetailsTask, ClientCareDetailsTask>()
                .ForMember(dto => dto.ClientCareDetailsTaskId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCareDetails, mem => mem.Ignore())
                .ForMember(dto => dto.Deleted, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCareDetailsHeading, mem => mem.Ignore());

            CreateMap<PutClientCareDetailsTask, ClientCareDetailsTask>()
                .ForMember(dto => dto.ClientCareDetails, mem => mem.Ignore())
               .ForMember(dto => dto.ClientCareDetailsHeading, mem => mem.Ignore());
            #endregion

            #region ClientCareDetails

            CreateMap<ClientCareDetails, GetClientCareDetails>();

            CreateMap<PostClientCareDetails, ClientCareDetails>()
               .ForMember(dto => dto.ClientCareDetailsId, mem => mem.Ignore())
               .ForMember(dto => dto.Client, mem => mem.Ignore())
               .ForMember(dto => dto.ClientCareDetailsTask, mem => mem.Ignore());
            #endregion

            #region StaffPersonalInfo
            CreateMap<StaffPersonalInfo, GetStaffPersonalInfo>()
                .ForMember(dto=>dto.Status,mem=>mem.MapFrom(src=>Enum.GetName(typeof(StaffRegistrationEnum),src.Status)));

            CreateMap<PostStaffPersonalInfo, StaffPersonalInfo>()
                .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.Ignore())
                .ForMember(dto => dto.Education, mem => mem.Ignore())
                .ForMember(dto => dto.Trainings, mem => mem.Ignore())
                .ForMember(dto => dto.References, mem => mem.Ignore())
                .ForMember(dto => dto.RegulatoryContact, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfoComments, mem => mem.Ignore())
                .ForMember(dto => dto.Status, mem => mem.MapFrom(src=> (int)StaffRegistrationEnum.Pending))
                .ForMember(dto => dto.RegistrationId, mem => mem.Ignore());

            CreateMap<PutStaffPersonalInfo, StaffPersonalInfo>()
               .ForMember(dto => dto.Education, mem => mem.Ignore())
               .ForMember(dto => dto.Trainings, mem => mem.Ignore())
               .ForMember(dto => dto.References, mem => mem.Ignore())
               .ForMember(dto => dto.RegulatoryContact, mem => mem.Ignore())
               .ForMember(dto => dto.StaffPersonalInfoComments, mem => mem.MapFrom(src=> (int)src.Status))
               .ForMember(dto => dto.Status, mem => mem.MapFrom(src=> (int)src.Status))
               .ForMember(dto => dto.RegistrationId, mem => mem.Ignore());

            CreateMap<PostStaffFullInfo, StaffPersonalInfo>()
                .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.Ignore())
                .ForMember(dto => dto.Education, mem => mem.MapFrom(src=>src.StaffEducations))
                .ForMember(dto => dto.Trainings, mem => mem.MapFrom(src=>src.StaffTrainings))
                .ForMember(dto => dto.References, mem => mem.MapFrom(sr=>sr.StaffReferees))
                .ForMember(dto => dto.RegulatoryContact, mem => mem.MapFrom(sr=>sr.StaffRegulatoryContacts))
                .ForMember(dto => dto.StaffPersonalInfoComments, mem => mem.Ignore())
                .ForMember(dto => dto.Status, mem => mem.MapFrom(src => (int)StaffRegistrationEnum.Pending))
                .ForMember(dto => dto.RegistrationId, mem => mem.Ignore());

            CreateMap<StaffPersonalInfo, GetStaffProfile>()
                .ForMember(dto=>dto.RegulatoryContacts,mem=>mem.MapFrom(src=>src.RegulatoryContact))
                 .ForMember(dto => dto.Status, mem => mem.MapFrom(src => Enum.GetName(typeof(StaffRegistrationEnum), src.Status)));

            CreateMap<StaffPersonalInfo, GetStaffs>()
                .ForMember(dto => dto.Fullname, mem => mem.MapFrom(src => string.Concat(src.FirstName, " ", src.MiddleName, " ", src.LastName)))
                .ForMember(dto => dto.Status, mem => mem.MapFrom(src => Enum.GetName(typeof(StaffRegistrationEnum), src.Status)));
            #endregion

            #region StaffTraining
            CreateMap<PostStaffTraining, StaffTraining>()
                .ForMember(dto => dto.StaffTrainingId, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            #endregion

            #region StaffEducation
            CreateMap<PostStaffEducation, StaffEducation>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.StaffEducationId, mem => mem.Ignore());
            #endregion

            #region StaffReferee
            CreateMap<PostStaffReferee, StaffReferee>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRefereeId, mem => mem.Ignore());

            CreateMap<PostStaffRegulatoryContact, StaffRegulatoryContact>()
               .ForMember(dto => dto.StaffRegulatoryContactId, mem => mem.Ignore())
               .ForMember(dto => dto.BaseRecordItem, mem => mem.Ignore())
               .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            #endregion

            #region StaffRegulatoryContact
            //  CreateMap<StaffRegulatoryContact, GetStaffRegulatoryContact>();
            #endregion

            #region StaffCommunication
            CreateMap<PostStaffCommunication, StaffCommunication>()
                .ForMember(dto=>dto.StaffCommunicationId,mem=>mem.Ignore());
            #endregion

            #region UnTowards
            CreateMap<PostUntowards, Untowards>()
                .ForMember(dto=>dto.UntowardsId,mem=>mem.Ignore())
                .ForMember(dto=>dto.TicketNumber,mem=>mem.Ignore())
                .ForMember(dto=>dto.StaffInvolved,mem=>mem.MapFrom(src=>src.StaffInvolved))
                .ForMember(dto=>dto.OfficerToAct,mem=>mem.MapFrom(src=>src.OfficerToAct));

            CreateMap<Untowards, GetUntowards>();
          
            #endregion

            #region UntowardsOfficerToAct
            CreateMap<PostUntowardsOfficerToAct, UntowardsOfficerToAct>()
                .ForMember(dto => dto.UntowardsId, mem => mem.Ignore())
                .ForMember(dto => dto.Untowards, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.UntowardsOfficerToActId, mem => mem.Ignore());
            #endregion

            #region UntowardsStaffInvolved
           
            CreateMap<PostUntowardsStaffInvolved, UntowardsStaffInvolved>()
                .ForMember(dto=>dto.StaffPersonalInfoId,mem=>mem.MapFrom(sr=>sr.StaffPersonalInfoId))
                .ForMember(dto => dto.UntowardsId, mem => mem.Ignore())
                .ForMember(dto => dto.Untowards, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.UntowardsStaffInvolvedId, mem => mem.Ignore());
            #endregion
        }
    }
}
