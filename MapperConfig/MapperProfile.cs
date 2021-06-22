﻿using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs;
using AwesomeCare.DataTransferObject.DTOs.ClientComplainRegister;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BaseRecordItem;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetails;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsTask;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using AwesomeCare.DataTransferObject.DTOs.ClientMedication;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaDays;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaTask;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using AwesomeCare.DataTransferObject.DTOs.Communication;
using AwesomeCare.DataTransferObject.DTOs.Company;
using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using AwesomeCare.DataTransferObject.DTOs.Investigation;
using AwesomeCare.DataTransferObject.DTOs.InvestigationAttachment;
using AwesomeCare.DataTransferObject.DTOs.Medication;
using AwesomeCare.DataTransferObject.DTOs.MedicationManufacturer;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using AwesomeCare.DataTransferObject.DTOs.RotaTask;
using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.ShiftBookingBlockedDays;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffBlackList;
using AwesomeCare.DataTransferObject.DTOs.StaffCommunication;
using AwesomeCare.DataTransferObject.DTOs.StaffRating;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using AwesomeCare.DataTransferObject.DTOs.StaffRotaPeriod;
using AwesomeCare.DataTransferObject.DTOs.StaffRotaTask;
using AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.StaffWorkTeam;
using AwesomeCare.DataTransferObject.DTOs.Untowards;
using AwesomeCare.DataTransferObject.DTOs.User;
using AwesomeCare.DataTransferObject.DTOs.ClientMealDays;
using AwesomeCare.DataTransferObject.DTOs.ClientLogAudit;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationAudit;
using AwesomeCare.DataTransferObject.DTOs.StaffSpotCheck;
using AwesomeCare.DataTransferObject.Enums;
using AwesomeCare.Model.Models;
using System;
using System.Linq;
using AwesomeCare.DataTransferObject.DTOs.ClientNutrition;
using AwesomeCare.DataTransferObject.DTOs.ClientMealType;
using AwesomeCare.DataTransferObject.DTOs.ClientShopping;
using AwesomeCare.DataTransferObject.DTOs.ClientCleaning;
using AwesomeCare.DataTransferObject.DTOs.ClientVoice;
using AwesomeCare.DataTransferObject.DTOs.ClientMgtVisit;
using AwesomeCare.DataTransferObject.DTOs.ClientProgram;
using AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch;
using AwesomeCare.DataTransferObject.DTOs.StaffOneToOne;
using AwesomeCare.DataTransferObject.DTOs.StaffAdlObs;
using AwesomeCare.DataTransferObject.DTOs.StaffKeyWorkerVoice;
using AwesomeCare.DataTransferObject.DTOs.StaffMedComp;
using AwesomeCare.DataTransferObject.DTOs.StaffSurvey;
using AwesomeCare.DataTransferObject.DTOs.StaffSupervisionAppraisal;
using AwesomeCare.DataTransferObject.DTOs.StaffReference;
using AwesomeCare.DataTransferObject.DTOs.Enotice;
using AwesomeCare.DataTransferObject.DTOs.Resources;
using AwesomeCare.DataTransferObject.DTOs.IncomingMeds;
using AwesomeCare.DataTransferObject.DTOs.WhisttleBlower;
using AwesomeCare.DataTransferObject.DTOs.ClientOxygenLvl;
using AwesomeCare.DataTransferObject.DTOs.ClientBMIChart;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodCoagulationRecord;
using AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement;
using AwesomeCare.DataTransferObject.DTOs.ClientFoodIntake;
using AwesomeCare.DataTransferObject.DTOs.ClientPainChart;
using AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring;
using AwesomeCare.DataTransferObject.DTOs.ClientHeartRate;
using AwesomeCare.DataTransferObject.DTOs.ClientPulseRate;
using AwesomeCare.DataTransferObject.DTOs.ClientSeizure;
using AwesomeCare.DataTransferObject.DTOs.ClientBodyTemp;
using AwesomeCare.DataTransferObject.DTOs.ClientWoundCare;

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
                .ForMember(dto => dto.RegulatoryContact, mem => mem.MapFrom(src => src.RegulatoryContacts))
                .ForMember(dto => dto.ClientCareDetails, mem => mem.MapFrom(src => src.CareDetails))
                .ForMember(dto => dto.InvolvingParties, mem => mem.MapFrom(src => src.InvolvingParties))
                .ForMember(dto => dto.ClientId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedication, mem => mem.Ignore())
                .ForMember(dto => dto.StaffBlackList, mem => mem.Ignore())
                .ForMember(dto => dto.Latitude, mem => mem.Ignore())
                .ForMember(dto => dto.Longitude, mem => mem.Ignore())
                .ForMember(dto => dto.ComplainRegister, mem => mem.Ignore())
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.UniqueId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedAudit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientLogAudit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientVoice, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMgtVisit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffSpotCheck, mem => mem.Ignore())
                .ForMember(dto => dto.StaffAdlObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffMedCompObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffKeyWorkerVoice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffReference, mem => mem.Ignore())
                .ForMember(dto => dto.ClientProgram, mem => mem.Ignore())
                .ForMember(dto => dto.ClientServiceWatch, mem => mem.Ignore())
                .ForMember(dto => dto.Enotice, mem => mem.Ignore())
                .ForMember(dto => dto.WhisttleBlower, mem => mem.Ignore())
                .ForMember(dto => dto.Resources, mem => mem.Ignore())
                .ForMember(dto => dto.IncomingMeds, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBloodPressure, mem => mem.Ignore())
                .ForMember(dto => dto.ClientFoodIntake, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.ClientPainChart, mem => mem.Ignore())
                .ForMember(dto => dto.ClientWoundCare, mem => mem.Ignore())
                .ForMember(dto => dto.ClientEyeHealthMonitoring, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBloodCoagulationRecord, mem => mem.Ignore())
                .ForMember(dto => dto.ClientSeizure, mem => mem.Ignore())
                .ForMember(dto => dto.ClientHeartRate, mem => mem.Ignore())
                .ForMember(dto => dto.ClientPulseRate, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBodyTemp, mem => mem.Ignore())
                .ForMember(dto => dto.ClientOxygenLvl, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBMIChart, mem => mem.Ignore());

            CreateMap<Client, GetClient>()
                .ForMember(dto => dto.QRCode, mem => mem.Ignore())
                .ForMember(dto => dto.Gender, mem => mem.Ignore())
                .ForMember(dto => dto.Status, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientMedAudit, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientLogAudit, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientComplain, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientVoice, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientMgtVisit, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientProgram, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientServiceWatch, mem => mem.Ignore());

            CreateMap<Client, GetClientDetail>()
               .ForMember(dto => dto.FullName, mem => mem.MapFrom(src => string.Concat(src.Firstname, " ", src.Middlename, " ", src.Surname)));




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
                .ForMember(dto => dto.ClientMedication, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCareDetails, mem => mem.Ignore())
                .ForMember(dto => dto.StaffBlackList, mem => mem.Ignore())
                .ForMember(dto => dto.RegulatoryContact, mem => mem.Ignore())
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.ComplainRegister, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedAudit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientLogAudit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientVoice, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMgtVisit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientProgram, mem => mem.Ignore())
                .ForMember(dto => dto.ClientServiceWatch, mem => mem.Ignore())
                .ForMember(dto => dto.StaffSpotCheck, mem => mem.Ignore())
                .ForMember(dto => dto.StaffAdlObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffMedCompObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffKeyWorkerVoice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffReference, mem => mem.Ignore())
                .ForMember(dto => dto.Enotice, mem => mem.Ignore())
                .ForMember(dto => dto.WhisttleBlower, mem => mem.Ignore())
                .ForMember(dto => dto.Resources, mem => mem.Ignore())
                .ForMember(dto => dto.IncomingMeds, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBloodPressure, mem => mem.Ignore())
                .ForMember(dto => dto.ClientFoodIntake, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.ClientPainChart, mem => mem.Ignore())
                .ForMember(dto => dto.ClientWoundCare, mem => mem.Ignore())
                .ForMember(dto => dto.ClientEyeHealthMonitoring, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBloodCoagulationRecord, mem => mem.Ignore())
                .ForMember(dto => dto.ClientSeizure, mem => mem.Ignore())
                .ForMember(dto => dto.ClientHeartRate, mem => mem.Ignore())
                .ForMember(dto => dto.ClientPulseRate, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBodyTemp, mem => mem.Ignore())
                .ForMember(dto => dto.ClientOxygenLvl, mem => mem.Ignore())
                .ForMember(dto => dto.ClientBMIChart, mem => mem.Ignore());
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
            CreateMap<ClientRegulatoryContact, GetClientRegulatoryContactForEdit>()
                .ForMember(dto=>dto.RegulatoryContact,mem=>mem.Ignore());
            #endregion

            #region ClientRotaName
            CreateMap<PostClientRotaName, Rota>()
                 .ForMember(dto => dto.ClientRotaDays, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRota, mem => mem.Ignore())
                .ForMember(dto => dto.ShiftBookings, mem => mem.Ignore())
                .ForMember(dto => dto.RotaId, mem => mem.Ignore());

            CreateMap<Rota, GetClientRotaName>();
            CreateMap<PutClientRotaName, Rota>()
                .ForMember(dto => dto.ClientRotaDays, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRota, mem => mem.Ignore())
                .ForMember(dto => dto.ShiftBookings, mem => mem.Ignore());
            #endregion

            #region ClientRotaType
            CreateMap<ClientRotaType, GetClientRotaType>();
            CreateMap<PutClientRotaType, ClientRotaType>()
                .ForMember(dto => dto.StaffRotaPeriods, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedicationPeriod, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore());

            CreateMap<PostClientRotaType, ClientRotaType>()
                .ForMember(dto => dto.RotaType, mem => mem.MapFrom(src => src.RotaType.ToUpper()))
                .ForMember(dto => dto.StaffRotaPeriods, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedicationPeriod, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaTypeId, mem => mem.Ignore());
            #endregion

            #region ClientMealType
            CreateMap<ClientMealType, GetClientMealType>();
            CreateMap<PutClientMealType, ClientMealType>()
                .ForMember(dto => dto.ClientMeal, mem => mem.Ignore());

            CreateMap<PostClientMealType, ClientMealType>()
                .ForMember(dto => dto.MealType, mem => mem.MapFrom(src => src.MealType.ToUpper()))
                .ForMember(dto => dto.ClientMeal, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMealTypeId, mem => mem.Ignore());
            #endregion

            #region RotaTask
            CreateMap<RotaTask, AwesomeCare.DataTransferObject.DTOs.RotaTask.GetRotaTask>();
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
                .ForMember(dto => dto.ClientMealDays, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedicationDay, mem => mem.Ignore())
                .ForMember(dto => dto.RotaDayofWeekId, mem => mem.Ignore());
            CreateMap<PutRotaDayofWeek, RotaDayofWeek>()
                .ForMember(dto => dto.ClientMedicationDay, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMealDays, mem => mem.Ignore())
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

            #region ClientNutrition
            CreateMap<ClientNutrition, GetClientNutrition>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientNutrition, ClientNutrition>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMealDays, mem => mem.Ignore());
            CreateMap<PutClientNutrition, ClientNutrition>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMealDays, mem => mem.Ignore())
                .ForMember(dto => dto.ClientShopping, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCleaning, mem => mem.Ignore());

            CreateMap<CreateNutrition, ClientNutrition>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            #endregion
            #region ClientRotaDays
            CreateMap<ClientRotaDays, GetClientRotaDays>();
            CreateMap<PostClientRotaDays, ClientRotaDays>()
                .ForMember(dto => dto.ClientRotaDaysId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
                .ForMember(dto => dto.Rota, mem => mem.Ignore())
                .ForMember(dto => dto.RotaDayofWeek, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaTask, mem => mem.Ignore());
            CreateMap<PutClientRotaDays, ClientRotaDays>()
               .ForMember(dto => dto.Rota, mem => mem.Ignore())
              .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
              .ForMember(dto => dto.RotaDayofWeek, mem => mem.Ignore())
              .ForMember(dto => dto.ClientRotaTask, mem => mem.Ignore());

            CreateMap<CreateClientRotaDays, ClientRotaDays>()
                 .ForMember(dto => dto.ClientRotaDaysId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRota, mem => mem.Ignore())
                .ForMember(dto => dto.Rota, mem => mem.Ignore())
                .ForMember(dto => dto.RotaDayofWeek, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaTask, mem => mem.MapFrom(src => src.RotaTasks));
            #endregion
            #region ClientMealDays
            CreateMap<ClientMealDays, GetClientMealDays>();
            CreateMap<PostClientMealDays, ClientMealDays>()
                .ForMember(dto => dto.ClientMealType, mem => mem.Ignore())
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.MealDayofWeek, mem => mem.Ignore());
            CreateMap<PutClientMealDays, ClientMealDays>()
              .ForMember(dto => dto.ClientMealType, mem => mem.Ignore())
              .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore())
              .ForMember(dto => dto.MealDayofWeek, mem => mem.Ignore());

            CreateMap<CreateClientMealDays, ClientMealDays>()
                 .ForMember(dto => dto.ClientMealType, mem => mem.Ignore())
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.MealDayofWeek, mem => mem.Ignore());
            #endregion
            #region Shopping
            CreateMap<CreateClientShopping, ClientShopping>()
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<ClientShopping, GetClientShopping>()
                .ForMember(dto => dto.ShoppingId, mem => mem.Ignore());
            CreateMap<PostClientShopping, ClientShopping>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore());
            #endregion
            #region Cleaning
            CreateMap<CreateClientCleaning, ClientCleaning>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore());
            CreateMap<ClientCleaning, GetClientCleaning>();
            CreateMap<PostClientCleaning, ClientCleaning>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore());
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
                .ForMember(dto => dto.Tasks, mem => mem.MapFrom(src => src.ClientCareDetailsTasks.Where(t => !t.Deleted)));
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
                .ForMember(dto => dto.Status, mem => mem.MapFrom(src => src.Status));// mem.MapFrom(src => Enum.GetName(typeof(StaffRegistrationEnum), src.Status)));

            CreateMap<PostStaffPersonalInfo, StaffPersonalInfo>()
                .ForMember(dto => dto.StaffWorkTeamId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffWorkTeam, mem => mem.Ignore())
                .ForMember(dto => dto.Education, mem => mem.Ignore())
                .ForMember(dto => dto.Trainings, mem => mem.Ignore())
                .ForMember(dto => dto.References, mem => mem.Ignore())
                .ForMember(dto => dto.RegulatoryContact, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfoComments, mem => mem.Ignore())
                .ForMember(dto => dto.EmergencyContacts, mem => mem.Ignore())
                .ForMember(dto => dto.ShiftBookings, mem => mem.Ignore())
                .ForMember(dto => dto.ApplicationUser, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.Ignore())
                .ForMember(dto => dto.IsTeamLeader, mem => mem.Ignore())
                .ForMember(dto => dto.HasUniform, mem => mem.Ignore())
                .ForMember(dto => dto.HasIdCard, mem => mem.Ignore())
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.EmploymentDate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRating, mem => mem.Ignore())
                .ForMember(dto => dto.StaffBlackList, mem => mem.Ignore())
                .ForMember(dto => dto.ApplicationUserId, mem => mem.MapFrom(src => src.ApplicationUserId))
                .ForMember(dto => dto.Status, mem => mem.MapFrom(src => (int)StaffRegistrationEnum.Pending))
                .ForMember(dto => dto.RegistrationId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedAudit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientLogAudit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientShopping, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCleaning, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMgtVisit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientProgram, mem => mem.Ignore())
                .ForMember(dto => dto.StaffSpotCheck, mem => mem.Ignore())
                .ForMember(dto => dto.StaffMedCompObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffAdlObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffKeyWorkerVoice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffSurvey, mem => mem.Ignore())
                .ForMember(dto => dto.StaffSupervisionAppraisal, mem => mem.Ignore())
                .ForMember(dto => dto.StaffOneToOne, mem => mem.Ignore())
                .ForMember(dto => dto.StaffReference, mem => mem.Ignore())
                .ForMember(dto => dto.ClientServiceWatch, mem => mem.Ignore())
                .ForMember(dto => dto.ClientVoice, mem => mem.Ignore());
            //.ForMember(dto => dto.ClientBloodPressure, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientFoodIntake, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientBowelMovement, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientPainChart, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientWoundCare, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientEyeHealthMonitoring, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientBloodCoagulationRecord, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientSeizure, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientHeartRate, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientPulseRate, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientBodyTemp, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientOxygenLvl, mem => mem.Ignore())
            //.ForMember(dto => dto.ClientBMIChart, mem => mem.Ignore());

            CreateMap<PutStaffPersonalInfo, StaffPersonalInfo>()
               .ForMember(dto => dto.StaffWorkTeam, mem => mem.Ignore())
               .ForMember(dto => dto.Education, mem => mem.MapFrom(src => src.Education))
               .ForMember(dto => dto.Trainings, mem => mem.MapFrom(src => src.Trainings))
               .ForMember(dto => dto.References, mem => mem.MapFrom(src => src.References))
               .ForMember(dto => dto.RegulatoryContact, mem => mem.Ignore())
               .ForMember(dto => dto.EmergencyContacts, mem => mem.MapFrom(src => src.EmergencyContacts))
               .ForMember(dto => dto.ShiftBookings, mem => mem.Ignore())
               .ForMember(dto => dto.ApplicationUser, mem => mem.Ignore())
               .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRating, mem => mem.Ignore())
                .ForMember(dto => dto.StaffBlackList, mem => mem.Ignore())
               .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.MapFrom(src => src.StaffPersonalInfoId))
               .ForMember(dto => dto.ApplicationUserId, mem => mem.MapFrom(src => src.ApplicationUserId))
               .ForMember(dto => dto.StaffPersonalInfoComments, mem => mem.MapFrom(src => (int)src.Status))
               .ForMember(dto => dto.Status, mem => mem.MapFrom(src => (int)src.Status))
               .ForMember(dto => dto.StaffPersonalInfoComments, mem => mem.Ignore())
               .ForMember(dto => dto.ClientShopping, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCleaning, mem => mem.Ignore())
               .ForMember(dto => dto.ClientMedAudit, mem => mem.Ignore())
               .ForMember(dto => dto.ClientLogAudit, mem => mem.Ignore())
               .ForMember(dto => dto.ClientMgtVisit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientProgram, mem => mem.Ignore())
                .ForMember(dto => dto.ClientServiceWatch, mem => mem.Ignore())
               .ForMember(dto => dto.ClientVoice, mem => mem.Ignore())
               .ForMember(dto => dto.StaffSpotCheck, mem => mem.Ignore())
               .ForMember(dto => dto.StaffMedCompObs, mem => mem.Ignore())
               .ForMember(dto => dto.StaffAdlObs, mem => mem.Ignore())
               .ForMember(dto => dto.StaffKeyWorkerVoice, mem => mem.Ignore())
               .ForMember(dto => dto.StaffSurvey, mem => mem.Ignore())
               .ForMember(dto => dto.StaffSupervisionAppraisal, mem => mem.Ignore())
               .ForMember(dto => dto.StaffOneToOne, mem => mem.Ignore())
               .ForMember(dto => dto.StaffReference, mem => mem.Ignore());
               //.ForMember(dto => dto.ClientBloodPressure, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientFoodIntake, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientBowelMovement, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientPainChart, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientWoundCare, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientEyeHealthMonitoring, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientBloodCoagulationRecord, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientSeizure, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientHeartRate, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientPulseRate, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientBodyTemp, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientOxygenLvl, mem => mem.Ignore())
               // .ForMember(dto => dto.ClientBMIChart, mem => mem.Ignore());

            CreateMap<PutStaffEducation, StaffEducation>()
                  .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<PostStaffFullInfo, StaffPersonalInfo>()
                .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffWorkTeamId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffWorkTeam, mem => mem.Ignore())
                .ForMember(dto => dto.ApplicationUser, mem => mem.Ignore())
                .ForMember(dto => dto.ApplicationUserId, mem => mem.MapFrom(src => src.ApplicationUserId))
                .ForMember(dto => dto.EmergencyContacts, mem => mem.MapFrom(src => src.EmergencyContacts))
                .ForMember(dto => dto.Education, mem => mem.MapFrom(src => src.StaffEducations))
                .ForMember(dto => dto.Trainings, mem => mem.MapFrom(src => src.StaffTrainings))
                .ForMember(dto => dto.References, mem => mem.MapFrom(sr => sr.StaffReferees))
                .ForMember(dto => dto.RegulatoryContact, mem => mem.MapFrom(sr => sr.StaffRegulatoryContacts))
                .ForMember(dto => dto.StaffPersonalInfoComments, mem => mem.Ignore())
                .ForMember(dto => dto.ShiftBookings, mem => mem.Ignore())
                .ForMember(dto => dto.IsTeamLeader, mem => mem.Ignore())
                .ForMember(dto => dto.ClientNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.HasUniform, mem => mem.Ignore())
                .ForMember(dto => dto.HasIdCard, mem => mem.Ignore())
                .ForMember(dto => dto.EmploymentDate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRating, mem => mem.Ignore())
                .ForMember(dto => dto.StaffBlackList, mem => mem.Ignore())
                .ForMember(dto => dto.Status, mem => mem.MapFrom(src => (int)StaffRegistrationEnum.Pending))
                .ForMember(dto => dto.RegistrationId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientShopping, mem => mem.Ignore())
                .ForMember(dto => dto.ClientCleaning, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedAudit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientLogAudit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMgtVisit, mem => mem.Ignore())
                .ForMember(dto => dto.ClientProgram, mem => mem.Ignore())
                .ForMember(dto => dto.ClientServiceWatch, mem => mem.Ignore())
                .ForMember(dto => dto.ClientVoice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffSpotCheck, mem => mem.Ignore())
                .ForMember(dto => dto.StaffMedCompObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffAdlObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffKeyWorkerVoice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffSurvey, mem => mem.Ignore())
                .ForMember(dto => dto.StaffSupervisionAppraisal, mem => mem.Ignore())
                .ForMember(dto => dto.StaffOneToOne, mem => mem.Ignore())
                .ForMember(dto => dto.StaffReference, mem => mem.Ignore());
                //.ForMember(dto => dto.ClientBloodPressure, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientFoodIntake, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientBowelMovement, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientPainChart, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientWoundCare, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientEyeHealthMonitoring, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientBloodCoagulationRecord, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientSeizure, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientHeartRate, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientPulseRate, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientBodyTemp, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientOxygenLvl, mem => mem.Ignore())
                //.ForMember(dto => dto.ClientBMIChart, mem => mem.Ignore());

            CreateMap<StaffPersonalInfo, GetStaffProfile>()
                .ForMember(dto => dto.GetStaffSpotCheck, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffMedComp, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffAdlObs, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffKeyWorkerVoice, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffSurvey, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffSupervisionAppraisal, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffOneToOne, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffReference, mem => mem.Ignore())
                .ForMember(dto => dto.RegulatoryContacts, mem => mem.MapFrom(src => src.RegulatoryContact))
                 .ForMember(dto => dto.Status, mem => mem.MapFrom(src => Enum.GetName(typeof(StaffRegistrationEnum), src.Status)));

            CreateMap<StaffPersonalInfo, GetStaffs>()
                .ForMember(dto => dto.Fullname, mem => mem.MapFrom(src => string.Concat(src.FirstName, " ", src.MiddleName, " ", src.LastName)))
                .ForMember(dto => dto.Status, mem => mem.MapFrom(src => src.Status.ToString()));// mem.MapFrom(src => Enum.GetName(typeof(StaffRegistrationEnum), src.Status)));
           
            
            #endregion

            #region StaffEmergencyContact
            CreateMap<PostStaffEmergencyContact, StaffEmergencyContact>()
                .ForMember(dto => dto.StaffEmergencyContactId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());

            CreateMap<PutStaffEmergencyContact, StaffEmergencyContact>()
               .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            #endregion

            #region StaffTraining
            CreateMap<PostStaffTraining, StaffTraining>()
                .ForMember(dto => dto.StaffTrainingId, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<PutStaffTraining, StaffTraining>()
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

            CreateMap<PutStaffReferee, StaffReferee>()
               .ForMember(dto => dto.Staff, mem => mem.Ignore());
            #endregion

            #region StaffRegulatoryContact
            //  CreateMap<StaffRegulatoryContact, GetStaffRegulatoryContact>();
            #endregion

            #region StaffCommunication
            CreateMap<PostStaffCommunication, StaffCommunication>()
                .ForMember(dto => dto.StaffCommunicationId, mem => mem.Ignore());
            #endregion

            #region UnTowards
            CreateMap<PostUntowards, Untowards>()
                .ForMember(dto => dto.UntowardsId, mem => mem.Ignore())
                .ForMember(dto => dto.TicketNumber, mem => mem.Ignore())
                .ForMember(dto => dto.StaffInvolved, mem => mem.MapFrom(src => src.StaffInvolved))
                .ForMember(dto => dto.OfficerToAct, mem => mem.MapFrom(src => src.OfficerToAct));

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
                .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.MapFrom(sr => sr.StaffPersonalInfoId))
                .ForMember(dto => dto.UntowardsId, mem => mem.Ignore())
                .ForMember(dto => dto.Untowards, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.UntowardsStaffInvolvedId, mem => mem.Ignore());
            #endregion

            #region ShiftBooking
            CreateMap<ShiftBooking, GetShiftBooking>();
            CreateMap<PostShiftBooking, ShiftBooking>()
                .ForMember(dto => dto.StaffShiftBooking, mem => mem.Ignore())
                .ForMember(dto => dto.ShiftBookingId, mem => mem.Ignore())
                .ForMember(dto => dto.ShiftBookingBlockedDays, mem => mem.Ignore());

            CreateMap<PostStaffShiftBooking, StaffShiftBooking>()
                .ForMember(dto => dto.ShiftBooking, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.StaffShiftBookingId, mem => mem.Ignore());

            CreateMap<StaffShiftBooking, GetStaffShiftBooking>();


            CreateMap<StaffShiftBookingDay, GetStaffShiftBookingDay>();

            CreateMap<PostStaffShiftBookingDay, StaffShiftBookingDay>()
                 .ForMember(dto => dto.StaffShiftBookingDayId, mem => mem.Ignore())
                .ForMember(dto => dto.ShiftBooking, mem => mem.Ignore());

            #endregion

            #region ShiftBookingBlockedDays
            CreateMap<PostShiftBookingBlockedDays,ShiftBookingBlockedDays>()
                .ForMember(dto=>dto.ShiftBookingBlockedDaysId,mem=>mem.Ignore())
                .ForMember(dto=>dto.ShiftBooking,mem=>mem.Ignore());

            CreateMap<ShiftBookingBlockedDays,GetShiftBookingBlockedDays>();
           
            #endregion

            #region StaffWorkTeam
            CreateMap<StaffWorkTeam, GetStaffWorkTeam>();
            CreateMap<PostStaffWorkTeam, StaffWorkTeam>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.StaffWorkTeamId, mem => mem.Ignore());
            CreateMap<PutStaffWorkTeam, StaffWorkTeam>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            #endregion

            #region Medication
            CreateMap<PostMedication, Medication>()
                .ForMember(dto => dto.MedicationId, mem => mem.Ignore())
                .ForMember(dto => dto.Deleted, mem => mem.MapFrom(src => false));

            CreateMap<Medication, GetMedication>();

            CreateMap<PutMedication, Medication>();
            #endregion

            #region Complain Register
            CreateMap<PutComplainRegister, ClientComplainRegister>();
            CreateMap<PostComplainRegister, ClientComplainRegister>();

            CreateMap<ClientComplainRegister, GetClientComplainRegister>();
            #endregion

            #region MedicationManufacturer
            CreateMap<PostMedicationManufacturer, MedicationManufacturer>()
                .ForMember(dto => dto.MedicationManufacturerId, mem => mem.Ignore())
                .ForMember(dto => dto.Deleted, mem => mem.MapFrom(src => false));

            CreateMap<MedicationManufacturer, GetMedicationManufacturer>();

            CreateMap<PutMedicationManufacturer, MedicationManufacturer>();
            #endregion

            #region ClientMedication
            CreateMap<PostClientMedication, ClientMedication>()
                .ForMember(dto => dto.ClientMedicationId, mem => mem.Ignore())
                .ForMember(dto => dto.MedicationManufacturer, mem => mem.Ignore())
                .ForMember(dto => dto.Medication, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedicationDay, mem => mem.MapFrom(src => src.ClientMedicationDay));

            CreateMap<PutClientMedication, ClientMedication>()
                .ForMember(dto => dto.MedicationManufacturer, mem => mem.Ignore())
                .ForMember(dto => dto.Medication, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedicationDay, mem => mem.MapFrom(src => src.ClientMedicationDay));

            CreateMap<ClientMedication, GetClientMedication>()
                 .ForMember(dto => dto.Medication, mem => mem.Ignore())
                 .ForMember(dto => dto.MedicationManufacturer, mem => mem.Ignore());
            #endregion

            #region ClientMedicationDay
            CreateMap<PostClientMedicationDay, ClientMedicationDay>()
                .ForMember(dto => dto.ClientMedicationDayId, mem => mem.Ignore())
                .ForMember(dto => dto.RotaDayofWeek, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedication, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedicationPeriod, mem => mem.MapFrom(src => src.ClientMedicationPeriod));

            CreateMap<PutClientMedicationDay, ClientMedicationDay>()
               .ForMember(dto => dto.RotaDayofWeek, mem => mem.Ignore())
               .ForMember(dto => dto.ClientMedication, mem => mem.Ignore())
               .ForMember(dto => dto.ClientMedicationPeriod, mem => mem.MapFrom(src => src.ClientMedicationPeriod));

            CreateMap<ClientMedicationDay, GetClientMedicationDay>()
                 .ForMember(dto => dto.DayOfWeek, mem => mem.Ignore());
            #endregion

            #region ClientMedicationPeriod
            CreateMap<PostClientMedicationPeriod, ClientMedicationPeriod>()
                .ForMember(dto => dto.ClientMedicationPeriodId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientMedicationDay, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaType, mem => mem.Ignore());

            CreateMap<PutClientMedicationPeriod, ClientMedicationPeriod>()
                .ForMember(dto => dto.ClientMedicationDay, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaType, mem => mem.Ignore());

            CreateMap<ClientMedicationPeriod, GetClientMedicationPeriod>()
                 .ForMember(dto => dto.RotaType, mem => mem.Ignore());
            #endregion

            #region StaffRota
            CreateMap<PostStaffRota, StaffRota>()
                .ForMember(dto => dto.StaffRotaId, mem => mem.Ignore())
                .ForMember(dto => dto.Rota, mem => mem.Ignore());
            #endregion

            #region StaffRotaPeriod
            CreateMap<PostStaffRotaPeriod, StaffRotaPeriod>()
                .ForMember(dto => dto.StaffRotaPeriodId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRota, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRotaId, mem => mem.Ignore())
                .ForMember(dto => dto.ClockInTime, mem => mem.Ignore())
                .ForMember(dto => dto.ClockOutTime, mem => mem.Ignore())
                .ForMember(dto => dto.ClockInAddress, mem => mem.Ignore())
                .ForMember(dto => dto.ClockOutAddress, mem => mem.Ignore())
                .ForMember(dto => dto.Feedback, mem => mem.Ignore())
                .ForMember(dto => dto.Comment, mem => mem.Ignore())
                .ForMember(dto => dto.HandOver, mem => mem.Ignore())
                .ForMember(dto => dto.ClockOutMode, mem => mem.Ignore())
                .ForMember(dto => dto.ClockInMode, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRotaTasks, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaType, mem => mem.Ignore());

            CreateMap<StaffRotaPeriod, GetStaffRotaPeriodForEdit>();
            CreateMap<GetStaffRotaPeriodForEdit, EditStaffRotaPeriod>();
            CreateMap<EditStaffRotaPeriod, StaffRotaPeriod>()
                .ForMember(dto => dto.StaffRotaId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaTypeId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRota, mem => mem.Ignore())
                .ForMember(dto => dto.ClockOutMode, mem => mem.Ignore())
                .ForMember(dto => dto.StartTime, mem => mem.Ignore())
                .ForMember(dto => dto.StopTime, mem => mem.Ignore())
                .ForMember(dto => dto.ClockInMode, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRotaTasks, mem => mem.Ignore())
                .ForMember(dto => dto.ClientRotaType, mem => mem.Ignore());
            //  CreateMap<StaffRotaPeriod, GetStaffRotaPeriod>();
            #endregion

            #region StaffRotaPartner
            CreateMap<PostStaffRotaPartner, StaffRotaPartner>()
                .ForMember(dto => dto.StaffRotaPartnerId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRotaId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRota, mem => mem.Ignore());

          
            #endregion

            #region StaffRotaItem
            CreateMap<PostStaffRotaItem, StaffRotaItem>()
                .ForMember(dto => dto.StaffRotaItemId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRotaId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRota, mem => mem.Ignore());
            #endregion

            #region StaffRotaDynamicAddition
            CreateMap<PostStaffRotaDynamicAddition, StaffRotaDynamicAddition>()
                .ForMember(dto => dto.StaffRotaDynamicAdditionId, mem => mem.Ignore());

            CreateMap<PutStaffRotaDynamicAddition, StaffRotaDynamicAddition>();

            CreateMap<GetStaffRotaDynamicAddition, StaffRotaDynamicAddition>();
            #endregion

            #region StaffRating
            CreateMap<PostStaffRating, StaffRating>()
                .ForMember(dto=>dto.StaffRatingId,mem=>mem.Ignore())
                .ForMember(dto=>dto.StaffPersonalInfo,mem=>mem.Ignore());
            #endregion

            #region StaffBlackList
            CreateMap<PostStaffBlackList, StaffBlackList>()
                .ForMember(dto => dto.StaffBlackListId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.Date, mem => mem.MapFrom(src=>DateTime.Now))
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<PutStaffBlackList, StaffBlackList>()
                .ForMember(dto => dto.Date, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<StaffBlackList, GetStaffBlackList>()
               .ForMember(dto => dto.Client, mem => mem.Ignore())
               .ForMember(dto => dto.Staff, mem => mem.Ignore());

            #endregion

            #region Communication
            CreateMap<PostCommunication, Communication>()
                .ForMember(dto => dto.CommunicationId, mem => mem.Ignore())
                .ForMember(dto => dto.IsRead, mem => mem.MapFrom(src => false))
                .ForMember(dto => dto.CommuncationDate, mem => mem.MapFrom(src => DateTime.Now))
                .ForMember(dto => dto.From, mem => mem.Ignore());
            #endregion

            #region StaffIncidentReporting
            CreateMap<PostReportStaff, StaffIncidentReporting>()
                .ForMember(dto => dto.StaffIncidentReportingId, mem => mem.Ignore())
                .ForMember(dto => dto.LoggedById, mem => mem.Ignore())
                .ForMember(dto => dto.LoggedDate, mem => mem.MapFrom(src => DateTimeOffset.Now));
            #endregion

            #region ClientServiceDetail
            CreateMap<PostClientServiceDetail, ClientServiceDetail>()
                .ForMember(dto => dto.ClientServiceDetailId, mem => mem.Ignore())
                .ForMember(dto => dto.ServiceDate, mem => mem.MapFrom(src => DateTimeOffset.Now));

            CreateMap<PostClientServiceDetailItem, ClientServiceDetailItem>()
                .ForMember(dto => dto.ClientServiceDetailItemId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientServiceDetail, mem => mem.Ignore());

            CreateMap<PostClientServiceDetailReceipt, ClientServiceDetailReceipt>()
                .ForMember(dto => dto.ClientServiceDetailReceiptId, mem => mem.Ignore())
                .ForMember(dto => dto.ClientServiceDetail, mem => mem.Ignore());
            #endregion

            #region Investigation
            CreateMap<PostInvestigation, Investigation>()
                .ForMember(dto => dto.InvestigationId, mem => mem.Ignore());

            CreateMap<PostInvestigationAttachment, InvestigationAttachment>()
                .ForMember(dto => dto.InvestigationAttachmentId, mem => mem.Ignore())
                .ForMember(dto => dto.Investigation, mem => mem.Ignore());
            #endregion

            #region StaffRotaTask
            CreateMap<PostStaffRotaTask, StaffRotaTask>()
                .ForMember(dto => dto.StaffRotaTaskId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffRotaPeriod, mem => mem.Ignore());

            CreateMap<StaffRotaTask, GetStaffRotaTask>();
            #endregion


            #region ApplicationUser
            CreateMap<ApplicationUser, GetUser>()
                .ForMember(dto => dto.UserId, mem => mem.MapFrom(src => src.Id))
                .ForMember(dto => dto.UserName, mem => mem.MapFrom(src => src.UserName))
                .ForMember(dto => dto.Email, mem => mem.MapFrom(src => src.Email))
                .ForMember(dto => dto.EmailConfirmed, mem => mem.MapFrom(src => src.EmailConfirmed))
                .ForMember(dto => dto.LockedOutEnabled, mem => mem.MapFrom(src => src.LockoutEnabled));

            #endregion

            #region Log Audit
            CreateMap<PutClientLogAudit, ClientLogAudit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostClientLogAudit, ClientLogAudit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<ClientLogAudit, GetClientLogAudit>();
            #endregion

            #region Medication Audit
            CreateMap<PutClientMedAudit, ClientMedAudit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostClientMedAudit, ClientMedAudit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<ClientMedAudit, GetClientMedAudit>();
            #endregion

            #region Voice
            CreateMap<PutClientVoice, ClientVoice>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostClientVoice, ClientVoice>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<ClientVoice, GetClientVoice>();
            #endregion

            #region MgtVisit
            CreateMap<PutClientMgtVisit, ClientMgtVisit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostClientMgtVisit, ClientMgtVisit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<ClientMgtVisit, GetClientMgtVisit>();
            #endregion
            #region Program
            CreateMap<PutClientProgram, ClientProgram>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostClientProgram, ClientProgram>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<ClientProgram, GetClientProgram>();
            #endregion
            #region ClientServiceWatch
            CreateMap<PutClientServiceWatch, ClientServiceWatch>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostClientServiceWatch, ClientServiceWatch>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<ClientServiceWatch, GetClientServiceWatch>();
            #endregion

            #region StaffSpotCheck
            CreateMap<PutStaffSpotCheck, StaffSpotCheck>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostStaffSpotCheck, StaffSpotCheck>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<StaffSpotCheck, GetStaffSpotCheck>();
            #endregion

            #region StaffAdlObs
            CreateMap<PutStaffAdlObs, StaffAdlObs>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostStaffAdlObs, StaffAdlObs>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<StaffAdlObs, GetStaffAdlObs>();
            #endregion

            #region StaffMedComp
            CreateMap<PutStaffMedComp, StaffMedComp>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostStaffMedComp, StaffMedComp>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<StaffMedComp, GetStaffMedComp>();
            #endregion

            #region StaffKeyWorkerVoice
            CreateMap<PutStaffKeyWorkerVoice, StaffKeyWorkerVoice>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostStaffKeyWorkerVoice, StaffKeyWorkerVoice>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<StaffKeyWorkerVoice, GetStaffKeyWorkerVoice>();
            #endregion



            #region StaffSurvey
            CreateMap<PutStaffSurvey, StaffSurvey>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostStaffSurvey, StaffSurvey>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<StaffSurvey, GetStaffSurvey>();
            #endregion

            #region StaffOneToOne
            CreateMap<PutStaffOneToOne, StaffOneToOne>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostStaffOneToOne, StaffOneToOne>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<StaffOneToOne, GetStaffOneToOne>();
            #endregion

            #region StaffSupervisionAppraisal
            CreateMap<PutStaffSupervisionAppraisal, StaffSupervisionAppraisal>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostStaffSupervisionAppraisal, StaffSupervisionAppraisal>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<StaffSupervisionAppraisal, GetStaffSupervisionAppraisal>();
            #endregion

            #region StaffReference
            CreateMap<PutStaffReference, StaffReference>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostStaffReference, StaffReference>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<StaffReference, GetStaffReference>();
            #endregion

            #region Enotice
            CreateMap<PutEnotice, Enotice>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostEnotice, Enotice>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<Enotice, GetEnotice>();
            #endregion

            #region Resources
            CreateMap<PutResources, Resources>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostResources, Resources>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<Resources, GetResources>();
            #endregion

            #region IncomingMeds
            CreateMap<PutIncomingMeds, IncomingMeds>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostIncomingMeds, IncomingMeds>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<IncomingMeds, GetIncomingMeds>();
            #endregion

            #region WhisttleBlower
            CreateMap<PutWhisttleBlower, WhisttleBlower>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostWhisttleBlower, WhisttleBlower>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<WhisttleBlower, GetWhisttleBlower>();
            #endregion

            #region BloodPressure
            CreateMap<PutClientBloodPressure, ClientBloodPressure>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientBloodPressure, ClientBloodPressure>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientBloodPressure, GetClientBloodPressure>();
            #endregion

            #region FoodIntake
            CreateMap<PutClientFoodIntake, ClientFoodIntake>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientFoodIntake, ClientFoodIntake>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientFoodIntake, GetClientFoodIntake>();
            #endregion

            #region BowelMovement
            CreateMap<PutClientBowelMovement, ClientBowelMovement>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientBowelMovement, ClientBowelMovement>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientBowelMovement, GetClientBowelMovement>();
            #endregion

            #region PainChart
            CreateMap<PutClientPainChart, ClientPainChart>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientPainChart, ClientPainChart>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientPainChart, GetClientPainChart>();
            #endregion

            #region WoundCare
            CreateMap<PutClientWoundCare, ClientWoundCare>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientWoundCare, ClientWoundCare>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientWoundCare, GetClientWoundCare>();
            #endregion

            #region EyeHealthMonitoring
            CreateMap<PutClientEyeHealthMonitoring, ClientEyeHealthMonitoring>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientEyeHealthMonitoring, ClientEyeHealthMonitoring>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientEyeHealthMonitoring, GetClientEyeHealthMonitoring>();
            #endregion

            #region BloodCoagulationRecord
            CreateMap<PutClientBloodCoagulationRecord, ClientBloodCoagulationRecord>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientBloodCoagulationRecord, ClientBloodCoagulationRecord>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientBloodCoagulationRecord, GetClientBloodCoagulationRecord>();
            #endregion

            #region HeartRate
            CreateMap<PutClientHeartRate, ClientHeartRate>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientHeartRate, ClientHeartRate>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientHeartRate, GetClientHeartRate>();
            #endregion

            #region PulseRate
            CreateMap<PutClientPulseRate, ClientPulseRate>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientPulseRate, ClientPulseRate>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientPulseRate, GetClientPulseRate>();
            #endregion

            #region Seizure
            CreateMap<PutClientSeizure, ClientSeizure>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientSeizure, ClientSeizure>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientSeizure, GetClientSeizure>();
            #endregion

            #region BodyTemp
            CreateMap<PutClientBodyTemp, ClientBodyTemp>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientBodyTemp, ClientBodyTemp>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientBodyTemp, GetClientBodyTemp>();
            #endregion

            #region OxygenLvl
            CreateMap<PutClientOxygenLvl, ClientOxygenLvl>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientOxygenLvl, ClientOxygenLvl>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientOxygenLvl, GetClientOxygenLvl>();
            #endregion

            #region BMIChart
            CreateMap<PutClientBMIChart, ClientBMIChart>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientBMIChart, ClientBMIChart>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientBMIChart, GetClientBMIChart>();
            #endregion

            #region BloodCoagOfficerToAct
            CreateMap<PostBloodCoagOfficerToAct, BloodCoagOfficerToAct>()
                .ForMember(dto => dto.BloodCoagulation, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodCoagOfficerToActId, mem => mem.Ignore());
            #endregion

            #region BloodCoagStaffName
            CreateMap<PostBloodCoagStaffName, BloodCoagStaffName>()
                .ForMember(dto => dto.BloodCoagulation, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodCoagStaffNameId, mem => mem.Ignore());
            #endregion

            #region BloodCoagPhysician
            CreateMap<PostBloodCoagPhysician, BloodCoagPhysician>()
                .ForMember(dto => dto.BloodCoagulation, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodCoagPhysicianId, mem => mem.Ignore());
            #endregion
        }
    }
}
