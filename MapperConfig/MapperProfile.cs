using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs;
using AwesomeCare.DataTransferObject.DTOs.ClientComplain;
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
using AwesomeCare.DataTransferObject.DTOs.StaffKeyWorker;
using AwesomeCare.DataTransferObject.DTOs.StaffMedComp;
using AwesomeCare.DataTransferObject.DTOs.StaffSurvey;
using AwesomeCare.DataTransferObject.DTOs.StaffSupervision;
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
using AwesomeCare.DataTransferObject.DTOs.ClientMedAudit;
using AwesomeCare.DataTransferObject.DTOs.ClientVisit;
using AwesomeCare.DataTransferObject.DTOs.ClientService;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail;
using AwesomeCare.DataTransferObject.DTOs.CarePlanNutrition;
using AwesomeCare.DataTransferObject.DTOs.Health.HealthAndLiving;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthCondition;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthAndMedication;
using AwesomeCare.DataTransferObject.DTOs.Health.Balance;
using AwesomeCare.DataTransferObject.DTOs.Health.PhysicalAbility;
using AwesomeCare.DataTransferObject.DTOs.Health.HistoryOfFall;
using AwesomeCare.DataTransferObject.DTOs.Health;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.PersonalHygiene;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.InfectionControl;
using AwesomeCare.DataTransferObject.DTOs.OfficeLocation;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.PersonalityTest;
using AwesomeCare.DataTransferObject.DTOs.Pets;
using AwesomeCare.DataTransferObject.DTOs.TaskBoard;
using AwesomeCare.DataTransferObject.DTOs.HospitalEntry;
using AwesomeCare.DataTransferObject.DTOs.HospitalExit;
using AwesomeCare.DataTransferObject.DTOs.StaffPersonalityTest;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment;
using AwesomeCare.DataTransferObject.DTOs.Staff.InfectionControl;
using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
using AwesomeCare.DataTransferObject.DTOs.TrackingConcernNote;
using AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest;
using AwesomeCare.DataTransferObject.DTOs.StaffShadowing;
using AwesomeCare.DataTransferObject.DTOs.StaffInterview;
using AwesomeCare.DataTransferObject.DTOs.StaffHealth;
using AwesomeCare.DataTransferObject.DTOs.PerformanceIndicator;
using AwesomeCare.DataTransferObject.DTOs.ClientDailyTask;
using AwesomeCare.DataTransferObject.DTOs.Staff.StaffHoliday;
using AwesomeCare.DataTransferObject.DTOs.SetupStaffHoliday;
using AwesomeCare.DataTransferObject.DTOs.StaffTeamLead;
using AwesomeCare.DataTransferObject.DTOs.StaffTeamLeadTasks;

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
                .ForMember(dto => dto.ClientBMIChart, mem => mem.Ignore())
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore())
                .ForMember(dto => dto.Balance, mem => mem.Ignore())
                .ForMember(dto => dto.HistoryOfFall, mem => mem.Ignore())
                .ForMember(dto => dto.HealthAndLiving, mem => mem.Ignore())
                .ForMember(dto => dto.PhysicalAbility, mem => mem.Ignore())
                .ForMember(dto => dto.SpecialHealthAndMedication, mem => mem.Ignore())
                .ForMember(dto => dto.SpecialHealthCondition, mem => mem.Ignore())
                .ForMember(dto => dto.PersonalHygiene, mem => mem.Ignore())
                .ForMember(dto => dto.InfectionControl, mem => mem.Ignore())
                .ForMember(dto => dto.ManagingTasks, mem => mem.Ignore())
                .ForMember(dto => dto.InterestAndObjective, mem => mem.Ignore())
                .ForMember(dto => dto.Pets, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalEntry, mem => mem.Ignore())
                .ForMember(dto => dto.CarePlanNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalExit, mem => mem.Ignore())
                .ForMember(dto => dto.HomeRiskAssessment, mem => mem.Ignore())
                .ForMember(dto => dto.DutyOnCall, mem => mem.Ignore())
                .ForMember(dto => dto.ClientDailyTask, mem => mem.Ignore())
                .ForMember(dto => dto.StaffTeamLead, mem => mem.Ignore());

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
                .ForMember(dto => dto.GetClientServiceWatch, mem => mem.Ignore())
                
                .ForMember(dto => dto.GetClientBloodCoagulationRecord, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientBloodPressure, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientBMIChart, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientBodyTemp, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientBowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientEyeHealthMonitoring, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientFoodIntake, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientHeartRate, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientOxygenLvl, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientPulseRate, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientSeizure, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientPainChart, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientWoundCare, mem => mem.Ignore())
                .ForMember(dto => dto.GetPets, mem => mem.Ignore())
                .ForMember(dto => dto.GetInterestAndObjective, mem => mem.Ignore())
                .ForMember(dto => dto.GetManagingTasks, mem => mem.Ignore())
                .ForMember(dto => dto.GetInfectionControl, mem => mem.Ignore())
                .ForMember(dto => dto.GetPersonalHygiene, mem => mem.Ignore())
                .ForMember(dto => dto.GetCarePlanNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.GetBalance, mem => mem.Ignore())
                .ForMember(dto => dto.GetPhysicalAbility, mem => mem.Ignore())
                .ForMember(dto => dto.GetHistoryOfFall, mem => mem.Ignore())
                .ForMember(dto => dto.GetHealthAndLiving, mem => mem.Ignore())
                .ForMember(dto => dto.GetSpecialHealthAndMedication, mem => mem.Ignore())
                .ForMember(dto => dto.GetSpecialHealthCondition, mem => mem.Ignore())
                .ForMember(dto => dto.GetReview, mem => mem.Ignore())
                .ForMember(dto => dto.GetHospitalEntry, mem => mem.Ignore())
                .ForMember(dto => dto.GetBaseRecords, mem => mem.Ignore())
                .ForMember(dto => dto.GetHospitalExit, mem => mem.Ignore())
                .ForMember(dto => dto.GetHomeRiskAssessment, mem => mem.Ignore())
                .ForMember(dto => dto.GetDutyOnCall, mem => mem.Ignore())
                .ForMember(dto => dto.GetClientDailyTask, mem => mem.Ignore());

            CreateMap<Client, GetClientDetail>()
               .ForMember(dto => dto.FullName, mem => mem.MapFrom(src => string.Concat(src.Firstname, " ", src.Middlename, " ", src.Surname)));

            CreateMap<Client, GetClientForEdit>()
                .ForMember(dto => dto.ClientImage, mem => mem.Ignore())
                .ForMember(dto => dto.InvolvingPartyCount, mem => mem.Ignore())
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
                .ForMember(dto => dto.ClientBMIChart, mem => mem.Ignore())
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore())
                .ForMember(dto => dto.Balance, mem => mem.Ignore())
                .ForMember(dto => dto.HistoryOfFall, mem => mem.Ignore())
                .ForMember(dto => dto.HealthAndLiving, mem => mem.Ignore())
                .ForMember(dto => dto.PhysicalAbility, mem => mem.Ignore())
                .ForMember(dto => dto.SpecialHealthAndMedication, mem => mem.Ignore())
                .ForMember(dto => dto.SpecialHealthCondition, mem => mem.Ignore())
                .ForMember(dto => dto.PersonalHygiene, mem => mem.Ignore())
                .ForMember(dto => dto.InfectionControl, mem => mem.Ignore())
                .ForMember(dto => dto.ManagingTasks, mem => mem.Ignore())
                .ForMember(dto => dto.InterestAndObjective, mem => mem.Ignore())
                .ForMember(dto => dto.Pets, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalEntry, mem => mem.Ignore())
                .ForMember(dto => dto.CarePlanNutrition, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalExit, mem => mem.Ignore())
                 .ForMember(dto => dto.HomeRiskAssessment, mem => mem.Ignore())
                 .ForMember(dto => dto.DutyOnCall, mem => mem.Ignore())
                 .ForMember(dto => dto.ClientDailyTask, mem => mem.Ignore())
                 .ForMember(dto => dto.StaffTeamLead, mem => mem.Ignore());
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
                .ForMember(dto => dto.RegulatoryContact, mem => mem.Ignore());
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
                .ForMember(dto => dto.ClientVoice, mem => mem.Ignore())
                .ForMember(dto => dto.Equipment, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalEntryPersonToTakeAction, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalEntryStaffInvolved, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalExitOfficerToTakeAction, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalityTest, mem => mem.Ignore())
                .ForMember(dto => dto.StaffInfectionControl, mem => mem.Ignore())
                .ForMember(dto => dto.StaffCompetenceTest, mem => mem.Ignore())
                .ForMember(dto => dto.StaffHealth, mem => mem.Ignore())
                .ForMember(dto => dto.StaffInterview, mem => mem.Ignore())
                .ForMember(dto => dto.StaffShadowing, mem => mem.Ignore())
                .ForMember(dto => dto.StaffHoliday, mem => mem.Ignore())
                .ForMember(dto => dto.SetupStaffHoliday, mem => mem.Ignore())
                .ForMember(dto => dto.StaffTeamLead, mem => mem.Ignore());


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
                .ForMember(dto => dto.StaffReference, mem => mem.Ignore())
                .ForMember(dto => dto.Equipment, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalEntryPersonToTakeAction, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalEntryStaffInvolved, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalExitOfficerToTakeAction, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalityTest, mem => mem.Ignore())
                .ForMember(dto => dto.StaffInfectionControl, mem => mem.Ignore())
                .ForMember(dto => dto.StaffCompetenceTest, mem => mem.Ignore())
                .ForMember(dto => dto.StaffHealth, mem => mem.Ignore())
                .ForMember(dto => dto.StaffInterview, mem => mem.Ignore())
                .ForMember(dto => dto.StaffShadowing, mem => mem.Ignore())
                .ForMember(dto => dto.StaffHoliday, mem => mem.Ignore())
                .ForMember(dto => dto.SetupStaffHoliday, mem => mem.Ignore())
                .ForMember(dto => dto.StaffTeamLead, mem => mem.Ignore());

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
                .ForMember(dto => dto.StaffReference, mem => mem.Ignore())
                .ForMember(dto => dto.Equipment, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalEntryPersonToTakeAction, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalEntryStaffInvolved, mem => mem.Ignore())
                .ForMember(dto => dto.HospitalExitOfficerToTakeAction, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalityTest, mem => mem.Ignore())
                .ForMember(dto => dto.StaffInfectionControl, mem => mem.Ignore())
                .ForMember(dto => dto.StaffCompetenceTest, mem => mem.Ignore())
                .ForMember(dto => dto.StaffHealth, mem => mem.Ignore())
                .ForMember(dto => dto.StaffInterview, mem => mem.Ignore())
                .ForMember(dto => dto.StaffShadowing, mem => mem.Ignore())
                .ForMember(dto => dto.StaffHoliday, mem => mem.Ignore())
                .ForMember(dto => dto.SetupStaffHoliday, mem => mem.Ignore())
                .ForMember(dto => dto.StaffTeamLead, mem => mem.Ignore());

            CreateMap<StaffPersonalInfo, GetStaffProfile>()
                .ForMember(dto => dto.GetStaffSpotCheck, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffMedComp, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffAdlObs, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffKeyWorkerVoice, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffSurvey, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffSupervisionAppraisal, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffOneToOne, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffReference, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffPersonalityTest, mem => mem.Ignore())
                .ForMember(dto => dto.RegulatoryContacts, mem => mem.MapFrom(src => src.RegulatoryContact))
                .ForMember(dto => dto.Status, mem => mem.MapFrom(src => Enum.GetName(typeof(StaffRegistrationEnum), src.Status)))
                .ForMember(dto => dto.GetStaffInfectionControl, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffCompetenceTest, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffHealth, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffInterview, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffShadowing, mem => mem.Ignore())
                .ForMember(dto => dto.GetStaffHoliday, mem => mem.Ignore());

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
            CreateMap<PostShiftBookingBlockedDays, ShiftBookingBlockedDays>()
                .ForMember(dto => dto.ShiftBookingBlockedDaysId, mem => mem.Ignore())
                .ForMember(dto => dto.ShiftBooking, mem => mem.Ignore());

            CreateMap<ShiftBookingBlockedDays, GetShiftBookingBlockedDays>();

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
            CreateMap<PutComplainRegister, ClientComplainRegister>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostComplainRegister, ClientComplainRegister>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

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
                .ForMember(dto => dto.BowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.FluidIntake, mem => mem.Ignore())
                .ForMember(dto => dto.OralCare, mem => mem.Ignore())
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
                .ForMember(dto => dto.BowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.FluidIntake, mem => mem.Ignore())
                .ForMember(dto => dto.OralCare, mem => mem.Ignore())
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
                .ForMember(dto => dto.StaffRatingId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            #endregion

            #region StaffBlackList
            CreateMap<PostStaffBlackList, StaffBlackList>()
                .ForMember(dto => dto.StaffBlackListId, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.Date, mem => mem.MapFrom(src => DateTime.Now))
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
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientLogAudit, ClientLogAudit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientLogAudit, GetClientLogAudit>();
            #endregion

            #region Medication Audit
            CreateMap<PutClientMedAudit, ClientMedAudit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientMedAudit, ClientMedAudit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientMedAudit, GetClientMedAudit>();
            #endregion

            #region Voice
            CreateMap<PutClientVoice, ClientVoice>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientVoice, ClientVoice>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientVoice, GetClientVoice>();
            #endregion

            #region MgtVisit
            CreateMap<PutClientMgtVisit, ClientMgtVisit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientMgtVisit, ClientMgtVisit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientMgtVisit, GetClientMgtVisit>();
            #endregion

            #region Program
            CreateMap<PutClientProgram, ClientProgram>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientProgram, ClientProgram>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientProgram, GetClientProgram>();
            #endregion

            #region ClientServiceWatch
            CreateMap<PutClientServiceWatch, ClientServiceWatch>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientServiceWatch, ClientServiceWatch>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientServiceWatch, GetClientServiceWatch>();
            #endregion

            #region StaffSpotCheck
            CreateMap<PutStaffSpotCheck, StaffSpotCheck>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostStaffSpotCheck, StaffSpotCheck>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<StaffSpotCheck, GetStaffSpotCheck>();
            #endregion

            #region StaffAdlObs
            CreateMap<PutStaffAdlObs, StaffAdlObs>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostStaffAdlObs, StaffAdlObs>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<StaffAdlObs, GetStaffAdlObs>();
            #endregion

            #region StaffMedComp
            CreateMap<PutStaffMedComp, StaffMedComp>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostStaffMedComp, StaffMedComp>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<StaffMedComp, GetStaffMedComp>();
            #endregion

            #region StaffKeyWorkerVoice
            CreateMap<PutStaffKeyWorkerVoice, StaffKeyWorkerVoice>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostStaffKeyWorkerVoice, StaffKeyWorkerVoice>()
                .ForMember(dto => dto.Staff, mem => mem.Ignore())
                .ForMember(dto => dto.Client, mem => mem.Ignore());

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

            #region Complain
            CreateMap<PostComplainOfficerToAct, ComplainOfficerToAct>()
                .ForMember(dto => dto.ComplainRegister, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ComplainOfficerToActId, mem => mem.Ignore());

            CreateMap<PostComplainStaffName, ComplainStaffName>()
                .ForMember(dto => dto.ComplainRegister, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ComplainStaffNameId, mem => mem.Ignore());

            CreateMap<PutComplainOfficerToAct, ComplainOfficerToAct>()
                .ForMember(dto => dto.ComplainRegister, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ComplainOfficerToActId, mem => mem.Ignore());

            CreateMap<PutComplainStaffName, ComplainStaffName>()
                .ForMember(dto => dto.ComplainRegister, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ComplainStaffNameId, mem => mem.Ignore());
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

            #region BloodCoag
            CreateMap<PostBloodCoagOfficerToAct, BloodCoagOfficerToAct>()
                .ForMember(dto => dto.BloodCoagulation, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodCoagOfficerToActId, mem => mem.Ignore());

            CreateMap<PostBloodCoagStaffName, BloodCoagStaffName>()
                .ForMember(dto => dto.BloodCoagulation, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodCoagStaffNameId, mem => mem.Ignore());

            CreateMap<PostBloodCoagPhysician, BloodCoagPhysician>()
                .ForMember(dto => dto.BloodCoagulation, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodCoagPhysicianId, mem => mem.Ignore());
            #endregion

            #region BloodPressure
            CreateMap<PostBloodPressureOfficerToAct, BloodPressureOfficerToAct>()
                .ForMember(dto => dto.BloodPressure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodPressureOfficerToActId, mem => mem.Ignore());

            CreateMap<PostBloodPressureStaffName, BloodPressureStaffName>()
                .ForMember(dto => dto.BloodPressure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodPressureStaffNameId, mem => mem.Ignore());

            CreateMap<PostBloodPressurePhysician, BloodPressurePhysician>()
                .ForMember(dto => dto.BloodPressure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodPressurePhysicianId, mem => mem.Ignore());

            #endregion

            #region BMIChart
            CreateMap<PostBMIChartOfficerToAct, BMIChartOfficerToAct>()
                .ForMember(dto => dto.BMIChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BMIChartOfficerToActId, mem => mem.Ignore());

            CreateMap<PostBMIChartStaffName, BMIChartStaffName>()
                .ForMember(dto => dto.BMIChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BMIChartStaffNameId, mem => mem.Ignore());

            CreateMap<PostBMIChartPhysician, BMIChartPhysician>()
                .ForMember(dto => dto.BMIChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BMIChartPhysicianId, mem => mem.Ignore());
            #endregion

            #region BodyTemp
            CreateMap<PostBodyTempOfficerToAct, BodyTempOfficerToAct>()
                .ForMember(dto => dto.BodyTemp, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BodyTempOfficerToActId, mem => mem.Ignore());

            CreateMap<PostBodyTempStaffName, BodyTempStaffName>()
                .ForMember(dto => dto.BodyTemp, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BodyTempStaffNameId, mem => mem.Ignore());

            CreateMap<PostBodyTempPhysician, BodyTempPhysician>()
                .ForMember(dto => dto.BodyTemp, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BodyTempPhysicianId, mem => mem.Ignore());
            #endregion

            #region BowelMovement
            CreateMap<PostBowelMovementOfficerToAct, BowelMovementOfficerToAct>()
                .ForMember(dto => dto.BowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BowelMovementOfficerToActId, mem => mem.Ignore());

            CreateMap<PostBowelMovementStaffName, BowelMovementStaffName>()
                .ForMember(dto => dto.BowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BowelMovementStaffNameId, mem => mem.Ignore());

            CreateMap<PostBowelMovementPhysician, BowelMovementPhysician>()
                .ForMember(dto => dto.BowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BowelMovementPhysicianId, mem => mem.Ignore());
            #endregion

            #region EyeHealth
            CreateMap<PostEyeHealthOfficerToAct, EyeHealthOfficerToAct>()
                .ForMember(dto => dto.EyeHealth, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.EyeHealthOfficerToActId, mem => mem.Ignore());

            CreateMap<PostEyeHealthStaffName, EyeHealthStaffName>()
                .ForMember(dto => dto.EyeHealth, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.EyeHealthStaffNameId, mem => mem.Ignore());

            CreateMap<PostEyeHealthPhysician, EyeHealthPhysician>()
                .ForMember(dto => dto.EyeHealth, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.EyeHealthPhysicianId, mem => mem.Ignore());
            #endregion

            #region FoodIntake
            CreateMap<PostFoodIntakeOfficerToAct, FoodIntakeOfficerToAct>()
                .ForMember(dto => dto.FoodIntake, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.FoodIntakeOfficerToActId, mem => mem.Ignore());

            CreateMap<PostFoodIntakeStaffName, FoodIntakeStaffName>()
                .ForMember(dto => dto.FoodIntake, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.FoodIntakeStaffNameId, mem => mem.Ignore());

            CreateMap<PostFoodIntakePhysician, FoodIntakePhysician>()
                .ForMember(dto => dto.FoodIntake, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.FoodIntakePhysicianId, mem => mem.Ignore());
            #endregion

            #region HeartRate
            CreateMap<PostHeartRateOfficerToAct, HeartRateOfficerToAct>()
                .ForMember(dto => dto.HeartRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.HeartRateOfficerToActId, mem => mem.Ignore());

            CreateMap<PostHeartRateStaffName, HeartRateStaffName>()
                .ForMember(dto => dto.HeartRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.HeartRateStaffNameId, mem => mem.Ignore());

            CreateMap<PostHeartRatePhysician, HeartRatePhysician>()
                .ForMember(dto => dto.HeartRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.HeartRatePhysicianId, mem => mem.Ignore());
            #endregion

            #region OxygenLvl
            CreateMap<PostOxygenLvlOfficerToAct, OxygenLvlOfficerToAct>()
                .ForMember(dto => dto.OxygenLvl, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.OxygenLvlOfficerToActId, mem => mem.Ignore());

            CreateMap<PostOxygenLvlStaffName, OxygenLvlStaffName>()
                .ForMember(dto => dto.OxygenLvl, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.OxygenLvlStaffNameId, mem => mem.Ignore());

            CreateMap<PostOxygenLvlPhysician, OxygenLvlPhysician>()
                .ForMember(dto => dto.OxygenLvl, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.OxygenLvlPhysicianId, mem => mem.Ignore());
            #endregion

            #region PainChart
            CreateMap<PostPainChartOfficerToAct, PainChartOfficerToAct>()
                .ForMember(dto => dto.PainChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PainChartOfficerToActId, mem => mem.Ignore());

            CreateMap<PostPainChartStaffName, PainChartStaffName>()
                .ForMember(dto => dto.PainChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PainChartStaffNameId, mem => mem.Ignore());

            CreateMap<PostPainChartPhysician, PainChartPhysician>()
                .ForMember(dto => dto.PainChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PainChartPhysicianId, mem => mem.Ignore());
            #endregion

            #region PulseRate

            CreateMap<PostPulseRateOfficerToAct, PulseRateOfficerToAct>()
                .ForMember(dto => dto.PulseRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PulseRateOfficerToActId, mem => mem.Ignore());

            CreateMap<PostPulseRateStaffName, PulseRateStaffName>()
                .ForMember(dto => dto.PulseRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PulseRateStaffNameId, mem => mem.Ignore());

            CreateMap<PostPulseRatePhysician, PulseRatePhysician>()
                .ForMember(dto => dto.PulseRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PulseRatePhysicianId, mem => mem.Ignore());
            #endregion

            #region Seizure

            CreateMap<PostSeizureOfficerToAct, SeizureOfficerToAct>()
                .ForMember(dto => dto.Seizure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SeizureOfficerToActId, mem => mem.Ignore());

            CreateMap<PostSeizureStaffName, SeizureStaffName>()
                .ForMember(dto => dto.Seizure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SeizureStaffNameId, mem => mem.Ignore());

            CreateMap<PostSeizurePhysician, SeizurePhysician>()
                .ForMember(dto => dto.Seizure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SeizurePhysicianId, mem => mem.Ignore());
            #endregion

            #region WoundCare

            CreateMap<PostWoundCareOfficerToAct, WoundCareOfficerToAct>()
                .ForMember(dto => dto.WoundCare, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.WoundCareOfficerToActId, mem => mem.Ignore());

            CreateMap<PostWoundCareStaffName, WoundCareStaffName>()
                .ForMember(dto => dto.WoundCare, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.WoundCareStaffNameId, mem => mem.Ignore());

            CreateMap<PostWoundCarePhysician, WoundCarePhysician>()
                .ForMember(dto => dto.WoundCare, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.WoundCarePhysicianId, mem => mem.Ignore());
            #endregion

            #region BloodCoag
            CreateMap<PutBloodCoagOfficerToAct, BloodCoagOfficerToAct>()
                .ForMember(dto => dto.BloodCoagulation, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodCoagOfficerToActId, mem => mem.Ignore());

            CreateMap<PutBloodCoagStaffName, BloodCoagStaffName>()
                .ForMember(dto => dto.BloodCoagulation, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodCoagStaffNameId, mem => mem.Ignore());

            CreateMap<PutBloodCoagPhysician, BloodCoagPhysician>()
                .ForMember(dto => dto.BloodCoagulation, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodCoagPhysicianId, mem => mem.Ignore());
            #endregion

            #region BloodPressure
            CreateMap<PutBloodPressureOfficerToAct, BloodPressureOfficerToAct>()
                .ForMember(dto => dto.BloodPressure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodPressureOfficerToActId, mem => mem.Ignore());

            CreateMap<PutBloodPressureStaffName, BloodPressureStaffName>()
                .ForMember(dto => dto.BloodPressure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodPressureStaffNameId, mem => mem.Ignore());

            CreateMap<PutBloodPressurePhysician, BloodPressurePhysician>()
                .ForMember(dto => dto.BloodPressure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BloodPressurePhysicianId, mem => mem.Ignore());

            #endregion

            #region BMIChart
            CreateMap<PutBMIChartOfficerToAct, BMIChartOfficerToAct>()
                .ForMember(dto => dto.BMIChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BMIChartOfficerToActId, mem => mem.Ignore());

            CreateMap<PutBMIChartStaffName, BMIChartStaffName>()
                .ForMember(dto => dto.BMIChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BMIChartStaffNameId, mem => mem.Ignore());

            CreateMap<PutBMIChartPhysician, BMIChartPhysician>()
                .ForMember(dto => dto.BMIChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BMIChartPhysicianId, mem => mem.Ignore());
            #endregion

            #region BodyTemp
            CreateMap<PutBodyTempOfficerToAct, BodyTempOfficerToAct>()
                .ForMember(dto => dto.BodyTemp, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BodyTempOfficerToActId, mem => mem.Ignore());

            CreateMap<PutBodyTempStaffName, BodyTempStaffName>()
                .ForMember(dto => dto.BodyTemp, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BodyTempStaffNameId, mem => mem.Ignore());

            CreateMap<PutBodyTempPhysician, BodyTempPhysician>()
                .ForMember(dto => dto.BodyTemp, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BodyTempPhysicianId, mem => mem.Ignore());
            #endregion

            #region BowelMovement
            CreateMap<PutBowelMovementOfficerToAct, BowelMovementOfficerToAct>()
                .ForMember(dto => dto.BowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BowelMovementOfficerToActId, mem => mem.Ignore());

            CreateMap<PutBowelMovementStaffName, BowelMovementStaffName>()
                .ForMember(dto => dto.BowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BowelMovementStaffNameId, mem => mem.Ignore());

            CreateMap<PutBowelMovementPhysician, BowelMovementPhysician>()
                .ForMember(dto => dto.BowelMovement, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.BowelMovementPhysicianId, mem => mem.Ignore());
            #endregion

            #region EyeHealth
            CreateMap<PutEyeHealthOfficerToAct, EyeHealthOfficerToAct>()
                .ForMember(dto => dto.EyeHealth, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.EyeHealthOfficerToActId, mem => mem.Ignore());

            CreateMap<PutEyeHealthStaffName, EyeHealthStaffName>()
                .ForMember(dto => dto.EyeHealth, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.EyeHealthStaffNameId, mem => mem.Ignore());

            CreateMap<PutEyeHealthPhysician, EyeHealthPhysician>()
                .ForMember(dto => dto.EyeHealth, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.EyeHealthPhysicianId, mem => mem.Ignore());
            #endregion

            #region FoodIntake
            CreateMap<PutFoodIntakeOfficerToAct, FoodIntakeOfficerToAct>()
                .ForMember(dto => dto.FoodIntake, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.FoodIntakeOfficerToActId, mem => mem.Ignore());

            CreateMap<PutFoodIntakeStaffName, FoodIntakeStaffName>()
                .ForMember(dto => dto.FoodIntake, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.FoodIntakeStaffNameId, mem => mem.Ignore());

            CreateMap<PutFoodIntakePhysician, FoodIntakePhysician>()
                .ForMember(dto => dto.FoodIntake, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.FoodIntakePhysicianId, mem => mem.Ignore());
            #endregion

            #region HeartRate
            CreateMap<PutHeartRateOfficerToAct, HeartRateOfficerToAct>()
                .ForMember(dto => dto.HeartRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.HeartRateOfficerToActId, mem => mem.Ignore());

            CreateMap<PutHeartRateStaffName, HeartRateStaffName>()
                .ForMember(dto => dto.HeartRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.HeartRateStaffNameId, mem => mem.Ignore());

            CreateMap<PutHeartRatePhysician, HeartRatePhysician>()
                .ForMember(dto => dto.HeartRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.HeartRatePhysicianId, mem => mem.Ignore());
            #endregion

            #region OxygenLvl
            CreateMap<PutOxygenLvlOfficerToAct, OxygenLvlOfficerToAct>()
                .ForMember(dto => dto.OxygenLvl, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.OxygenLvlOfficerToActId, mem => mem.Ignore());

            CreateMap<PutOxygenLvlStaffName, OxygenLvlStaffName>()
                .ForMember(dto => dto.OxygenLvl, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.OxygenLvlStaffNameId, mem => mem.Ignore());

            CreateMap<PutOxygenLvlPhysician, OxygenLvlPhysician>()
                .ForMember(dto => dto.OxygenLvl, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.OxygenLvlPhysicianId, mem => mem.Ignore());
            #endregion

            #region PainChart
            CreateMap<PutPainChartOfficerToAct, PainChartOfficerToAct>()
                .ForMember(dto => dto.PainChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PainChartOfficerToActId, mem => mem.Ignore());

            CreateMap<PutPainChartStaffName, PainChartStaffName>()
                .ForMember(dto => dto.PainChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PainChartStaffNameId, mem => mem.Ignore());

            CreateMap<PutPainChartPhysician, PainChartPhysician>()
                .ForMember(dto => dto.PainChart, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PainChartPhysicianId, mem => mem.Ignore());
            #endregion

            #region PulseRate

            CreateMap<PutPulseRateOfficerToAct, PulseRateOfficerToAct>()
                .ForMember(dto => dto.PulseRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PulseRateOfficerToActId, mem => mem.Ignore());

            CreateMap<PutPulseRateStaffName, PulseRateStaffName>()
                .ForMember(dto => dto.PulseRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PulseRateStaffNameId, mem => mem.Ignore());

            CreateMap<PutPulseRatePhysician, PulseRatePhysician>()
                .ForMember(dto => dto.PulseRate, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.PulseRatePhysicianId, mem => mem.Ignore());
            #endregion

            #region Seizure

            CreateMap<PutSeizureOfficerToAct, SeizureOfficerToAct>()
                .ForMember(dto => dto.Seizure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SeizureOfficerToActId, mem => mem.Ignore());

            CreateMap<PutSeizureStaffName, SeizureStaffName>()
                .ForMember(dto => dto.Seizure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SeizureStaffNameId, mem => mem.Ignore());

            CreateMap<PutSeizurePhysician, SeizurePhysician>()
                .ForMember(dto => dto.Seizure, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SeizurePhysicianId, mem => mem.Ignore());
            #endregion

            #region WoundCare

            CreateMap<PutWoundCareOfficerToAct, WoundCareOfficerToAct>()
                .ForMember(dto => dto.WoundCare, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.WoundCareOfficerToActId, mem => mem.Ignore());

            CreateMap<PutWoundCareStaffName, WoundCareStaffName>()
                .ForMember(dto => dto.WoundCare, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.WoundCareStaffNameId, mem => mem.Ignore());

            CreateMap<PutWoundCarePhysician, WoundCarePhysician>()
                .ForMember(dto => dto.WoundCare, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.WoundCarePhysicianId, mem => mem.Ignore());
            #endregion

            #region Multi-Log Audit
            CreateMap<PostLogAuditOfficerToAct, LogAuditOfficerToAct>()
                .ForMember(dto => dto.LogAudit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.LogAuditOfficerToActId, mem => mem.Ignore());

            CreateMap<PutLogAuditOfficerToAct, LogAuditOfficerToAct>()
                .ForMember(dto => dto.LogAudit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.LogAuditOfficerToActId, mem => mem.Ignore());
            #endregion

            #region Multi-Med Audit
            CreateMap<PostMedAuditOfficerToAct, MedAuditOfficerToAct>()
                .ForMember(dto => dto.MedAudit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.MedAuditOfficerToActId, mem => mem.Ignore());

            CreateMap<PostMedAuditStaffName, MedAuditStaffName>()
                .ForMember(dto => dto.MedAudit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.MedAuditStaffNameId, mem => mem.Ignore());

            CreateMap<PutMedAuditOfficerToAct, MedAuditOfficerToAct>()
                .ForMember(dto => dto.MedAudit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.MedAuditOfficerToActId, mem => mem.Ignore());

            CreateMap<PutMedAuditStaffName, MedAuditStaffName>()
                .ForMember(dto => dto.MedAudit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.MedAuditStaffNameId, mem => mem.Ignore());
            #endregion

            #region Multi-Mgt Visit
            CreateMap<PostVisitOfficerToAct, VisitOfficerToAct>()
                .ForMember(dto => dto.Visit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VisitOfficerToActId, mem => mem.Ignore());

            CreateMap<PostVisitStaffName, VisitStaffName>()
                .ForMember(dto => dto.Visit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VisitStaffNameId, mem => mem.Ignore());

            CreateMap<PutVisitOfficerToAct, VisitOfficerToAct>()
                .ForMember(dto => dto.Visit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VisitOfficerToActId, mem => mem.Ignore());

            CreateMap<PutVisitStaffName, VisitStaffName>()
                .ForMember(dto => dto.Visit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VisitStaffNameId, mem => mem.Ignore());
            #endregion

            #region Multi-Program
            CreateMap<PostProgramOfficerToAct, ProgramOfficerToAct>()
                .ForMember(dto => dto.Program, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ProgramOfficerToActId, mem => mem.Ignore());

            CreateMap<PutProgramOfficerToAct, ProgramOfficerToAct>()
                .ForMember(dto => dto.Program, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ProgramOfficerToActId, mem => mem.Ignore());
            #endregion

            #region Multi-Service Watch
            CreateMap<PostServiceOfficerToAct, ServiceOfficerToAct>()
                .ForMember(dto => dto.Service, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ServiceOfficerToActId, mem => mem.Ignore());

            CreateMap<PostServiceStaffName, ServiceStaffName>()
                .ForMember(dto => dto.Service, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ServiceStaffNameId, mem => mem.Ignore());

            CreateMap<PutServiceOfficerToAct, ServiceOfficerToAct>()
                .ForMember(dto => dto.Service, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ServiceOfficerToActId, mem => mem.Ignore());

            CreateMap<PutServiceStaffName, ServiceStaffName>()
                .ForMember(dto => dto.Service, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.ServiceStaffNameId, mem => mem.Ignore());
            #endregion

            #region Multi-Voice
            CreateMap<PostVoiceOfficerToAct, VoiceOfficerToAct>()
                .ForMember(dto => dto.Voice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VoiceOfficerToActId, mem => mem.Ignore());

            CreateMap<PostVoiceCallerName, VoiceCallerName>()
                .ForMember(dto => dto.Voice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VoiceCallerNameId, mem => mem.Ignore());

            CreateMap<PostVoiceGoodStaff, VoiceGoodStaff>()
               .ForMember(dto => dto.Voice, mem => mem.Ignore())
               .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
               .ForMember(dto => dto.VoiceGoodStaffId, mem => mem.Ignore());

            CreateMap<PostVoicePoorStaff, VoicePoorStaff>()
                .ForMember(dto => dto.Voice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VoicePoorStaffId, mem => mem.Ignore());

            CreateMap<PutVoiceOfficerToAct, VoiceOfficerToAct>()
                .ForMember(dto => dto.Voice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VoiceOfficerToActId, mem => mem.Ignore());

            CreateMap<PutVoiceCallerName, VoiceCallerName>()
                .ForMember(dto => dto.Voice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VoiceCallerNameId, mem => mem.Ignore());

            CreateMap<PutVoiceGoodStaff, VoiceGoodStaff>()
                .ForMember(dto => dto.Voice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VoiceGoodStaffId, mem => mem.Ignore());

            CreateMap<PutVoicePoorStaff, VoicePoorStaff>()
                .ForMember(dto => dto.Voice, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.VoicePoorStaffId, mem => mem.Ignore());
            #endregion

            #region Multi-Survey
            CreateMap<PostSurveyOfficerToAct, SurveyOfficerToAct>()
                .ForMember(dto => dto.Survey, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SurveyOfficerToActId, mem => mem.Ignore());

            CreateMap<PutSurveyOfficerToAct, SurveyOfficerToAct>()
                .ForMember(dto => dto.Survey, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SurveyOfficerToActId, mem => mem.Ignore());

            CreateMap<PostSurveyWorkteam, SurveyWorkteam>()
               .ForMember(dto => dto.Survey, mem => mem.Ignore())
               .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
               .ForMember(dto => dto.SurveyWorkteamId, mem => mem.Ignore());

            CreateMap<PutSurveyWorkteam, SurveyWorkteam>()
                .ForMember(dto => dto.Survey, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SurveyWorkteamId, mem => mem.Ignore());
            #endregion

            #region Multi-Supervision Appraisal
            CreateMap<PostSupervisionOfficerToAct, SupervisionOfficerToAct>()
                .ForMember(dto => dto.Supervision, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SupervisionOfficerToActId, mem => mem.Ignore());

            CreateMap<PutSupervisionOfficerToAct, SupervisionOfficerToAct>()
                .ForMember(dto => dto.Supervision, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SupervisionOfficerToActId, mem => mem.Ignore());

            CreateMap<PostSupervisionWorkteam, SupervisionWorkteam>()
               .ForMember(dto => dto.Supervision, mem => mem.Ignore())
               .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
               .ForMember(dto => dto.SupervisionWorkteamId, mem => mem.Ignore());

            CreateMap<PutSupervisionWorkteam, SupervisionWorkteam>()
                .ForMember(dto => dto.Supervision, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SupervisionWorkteamId, mem => mem.Ignore());
            #endregion

            #region Multi-KeyWorker
            CreateMap<PostKeyWorkerOfficerToAct, KeyWorkerOfficerToAct>()
                .ForMember(dto => dto.KeyWorker, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.KeyWorkerOfficerToActId, mem => mem.Ignore());

            CreateMap<PutKeyWorkerOfficerToAct, KeyWorkerOfficerToAct>()
                .ForMember(dto => dto.KeyWorker, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.KeyWorkerOfficerToActId, mem => mem.Ignore());

            CreateMap<PostKeyWorkerWorkteam, KeyWorkerWorkteam>()
               .ForMember(dto => dto.KeyWorker, mem => mem.Ignore())
               .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
               .ForMember(dto => dto.KeyWorkerWorkteamId, mem => mem.Ignore());

            CreateMap<PutKeyWorkerWorkteam, KeyWorkerWorkteam>()
                .ForMember(dto => dto.KeyWorker, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.KeyWorkerWorkteamId, mem => mem.Ignore());
            #endregion

            #region Multi-SpotCheck
            CreateMap<PostSpotCheckOfficerToAct, SpotCheckOfficerToAct>()
                .ForMember(dto => dto.SpotCheck, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SpotCheckOfficerToActId, mem => mem.Ignore());

            CreateMap<PutSpotCheckOfficerToAct, SpotCheckOfficerToAct>()
                .ForMember(dto => dto.SpotCheck, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.SpotCheckOfficerToActId, mem => mem.Ignore());
            #endregion

            #region Multi-OneToOne
            CreateMap<PostOneToOneOfficerToAct, OneToOneOfficerToAct>()
                .ForMember(dto => dto.OneToOne, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.OneToOneOfficerToActId, mem => mem.Ignore());

            CreateMap<PutOneToOneOfficerToAct, OneToOneOfficerToAct>()
                .ForMember(dto => dto.OneToOne, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.OneToOneOfficerToActId, mem => mem.Ignore());
            #endregion

            #region Multi-MedComp
            CreateMap<PostMedCompOfficerToAct, MedCompOfficerToAct>()
                .ForMember(dto => dto.MedComp, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.MedCompOfficerToActId, mem => mem.Ignore());

            CreateMap<PutMedCompOfficerToAct, MedCompOfficerToAct>()
                .ForMember(dto => dto.MedComp, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.MedCompOfficerToActId, mem => mem.Ignore());
            #endregion

            #region Multi-AdlObs
            CreateMap<PostAdlObsOfficerToAct, AdlObsOfficerToAct>()
                .ForMember(dto => dto.AdlObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.AdlObsOfficerToActId, mem => mem.Ignore());

            CreateMap<PutAdlObsOfficerToAct, AdlObsOfficerToAct>()
                .ForMember(dto => dto.AdlObs, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.AdlObsOfficerToActId, mem => mem.Ignore());
            #endregion

            #region Capacity
            CreateMap<PutCapacity, Capacity>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<PostCapacity, Capacity>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<Capacity, GetCapacity>();

            CreateMap<PostCapacityIndicator, CapacityIndicator>()
            .ForMember(dto => dto.Capacity, mem => mem.Ignore())
            .ForMember(dto => dto.CapacityIndicatorId, mem => mem.Ignore());
            #endregion

            #region ConsentCare
            CreateMap<PutConsentCare, ConsentCare>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<PostConsentCare, ConsentCare>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());

            CreateMap<ConsentCare, GetConsentCare>();
            #endregion

            #region ConsentData
            CreateMap<PutConsentData, ConsentData>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<PostConsentData, ConsentData>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());

            CreateMap<ConsentData, GetConsentData>();
            #endregion

            #region ConsentLandLine
            CreateMap<PutConsentLandLine, ConsentLandLine>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<PostConsentLandLine, ConsentLandLine>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<ConsentLandLine, GetConsentLandLine>();

            CreateMap<PostConsentLandlineLog, ConsentLandlineLog>()
            .ForMember(dto => dto.ConsentLandLine, mem => mem.Ignore())
            .ForMember(dto => dto.ConsentLandlineLogId, mem => mem.Ignore());
            #endregion

            #region Equipment
            CreateMap<PutEquipment, Equipment>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());
            CreateMap<PostEquipment, Equipment>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore())
                .ForMember(dto => dto.Staff, mem => mem.Ignore());

            CreateMap<Equipment, GetEquipment>();
            #endregion

            #region KeyIndicators
            CreateMap<PutKeyIndicators, KeyIndicators>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<PostKeyIndicators, KeyIndicators>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<KeyIndicators, GetKeyIndicators>();

            CreateMap<PostKeyIndicatorLog, KeyIndicatorLog>()
            .ForMember(dto => dto.KeyIndicators, mem => mem.Ignore())
            .ForMember(dto => dto.KeyIndicatorLogId, mem => mem.Ignore());
            #endregion

            #region Personal
            CreateMap<PutPersonal, Personal>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<PostPersonal, Personal>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());

            CreateMap<Personal, GetPersonal>();
            #endregion

            #region PersonCentred
            CreateMap<PutPersonCentred, PersonCentred>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<PostPersonCentred, PersonCentred>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());

            CreateMap<PersonCentred, GetPersonCentred>();

            CreateMap<PostPersonCentredFocus, PersonCentredFocus>()
            .ForMember(dto => dto.PersonCentre, mem => mem.Ignore())
            .ForMember(dto => dto.PersonCentredFocusId, mem => mem.Ignore());
            #endregion

            #region Review
            CreateMap<PutReview, Review>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());
            CreateMap<PostReview, Review>()
                .ForMember(dto => dto.PersonalDetail, mem => mem.Ignore());

            CreateMap<Review, GetReview>();
            #endregion

            #region PersonalDetails
            CreateMap<PutPersonalDetail, PersonalDetail>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostPersonalDetail, PersonalDetail>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PersonalDetail, GetPersonalDetail>();
            #endregion

            #region CarePlanNutrition
            CreateMap<PutCarePlanNutrition, CarePlanNutrition>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostCarePlanNutrition, CarePlanNutrition>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<CarePlanNutrition, GetCarePlanNutrition>();
            #endregion

            #region HealthAndLiving
            CreateMap<PutHealthAndLiving, HealthAndLiving>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostHealthAndLiving, HealthAndLiving>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<HealthAndLiving, GetHealthAndLiving>();
            #endregion

            #region SpecialHealthCondition
            CreateMap<PutSpecialHealthCondition, SpecialHealthCondition>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostSpecialHealthCondition, SpecialHealthCondition>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<SpecialHealthCondition, GetSpecialHealthCondition>();
            #endregion

            #region SpecialHealthAndMedication
            CreateMap<PutSpecialHealthAndMedication, SpecialHealthAndMedication>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostSpecialHealthAndMedication, SpecialHealthAndMedication>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<SpecialHealthAndMedication, GetSpecialHealthAndMedication>();
            #endregion

            #region Balance
            CreateMap<PutBalance, Balance>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostBalance, Balance>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<Balance, GetBalance>();
            #endregion

            #region PhysicalAbility
            CreateMap<PutPhysicalAbility, PhysicalAbility>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostPhysicalAbility, PhysicalAbility>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<PhysicalAbility, GetPhysicalAbility>();
            #endregion

            #region HistoryOfFall
            CreateMap<PutHistoryOfFall, HistoryOfFall>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostHistoryOfFall, HistoryOfFall>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<HistoryOfFall, GetHistoryOfFall>();
            #endregion
            
            #region PersonalHygiene
            CreateMap<PutPersonalHygiene, PersonalHygiene>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostPersonalHygiene, PersonalHygiene>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<PersonalHygiene, GetPersonalHygiene>();
            #endregion

            #region InfectionControl
            CreateMap<PutInfectionControl, InfectionControl>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostInfectionControl, InfectionControl>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<InfectionControl, GetInfectionControl>();
            #endregion

            #region OfficeLocation
            CreateMap<PostOfficeLocation, OfficeLocation>()
                .ForMember(dto => dto.OfficeLocationId, mem => mem.Ignore())
                .ForMember(dto => dto.UniqueId, mem => mem.Ignore());

            CreateMap<PutOfficeLocation, OfficeLocation>();

            CreateMap<OfficeLocation, GetOfficeLocation>();
            #endregion

            #region ManagingTasks
            CreateMap<PutManagingTasks, ManagingTasks>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostManagingTasks, ManagingTasks>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ManagingTasks, GetManagingTasks>();
            #endregion

            #region InterestAndObjective
            CreateMap<PutInterestAndObjective, InterestAndObjective>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostInterestAndObjective, InterestAndObjective>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<InterestAndObjective, GetInterestAndObjective>();
            #endregion

            #region Interest
            CreateMap<PutInterest, Interest>()
                .ForMember(dto => dto.InterestAndObjective, mem => mem.Ignore());
            CreateMap<PostInterest, Interest>()
                .ForMember(dto => dto.InterestAndObjective, mem => mem.Ignore());

            CreateMap<Interest, GetInterest>();
            #endregion

            #region PersonalityTest
            CreateMap<PutPersonalityTest, PersonalityTest>()
                .ForMember(dto => dto.InterestAndObjective, mem => mem.Ignore());
            CreateMap<PostPersonalityTest, PersonalityTest>()
                .ForMember(dto => dto.InterestAndObjective, mem => mem.Ignore());

            CreateMap<PersonalityTest, GetPersonalityTest>();
            #endregion

            #region Pets
            CreateMap<PutPets, Pets>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostPets, Pets>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<Pets, GetPets>();
            #endregion

            #region TaskBoard
            CreateMap<PutTaskBoard, TaskBoard>();
            CreateMap<PostTaskBoard, TaskBoard>();
            CreateMap<TaskBoard, GetTaskBoard>();

            CreateMap<PutTaskBoardAssignedTo, TaskBoardAssignedTo>()
                .ForMember(dto => dto.TaskBoard, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostTaskBoardAssignedTo, TaskBoardAssignedTo>()
                .ForMember(dto => dto.TaskBoard, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<TaskBoardAssignedTo, GetTaskBoardAssignedTo>()
                .ForMember(dto => dto.StaffName, mem => mem.Ignore());
            #endregion

            #region HospitalEntry
            CreateMap<PutHospitalEntry, HospitalEntry>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostHospitalEntry, HospitalEntry>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<HospitalEntry, GetHospitalEntry>();

            CreateMap<PostHospitalEntryStaffInvolved, HospitalEntryStaffInvolved>()
                .ForMember(dto => dto.HospitalEntry, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PutHospitalEntryStaffInvolved, HospitalEntryStaffInvolved>()
                .ForMember(dto => dto.HospitalEntry, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<HospitalEntryStaffInvolved, GetHospitalEntryStaffInvolved>()
                .ForMember(dto => dto.StaffName, mem => mem.Ignore());
            CreateMap<PostHospitalEntryPersonToTakeAction, HospitalEntryPersonToTakeAction>()
                .ForMember(dto => dto.HospitalEntry, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PutHospitalEntryPersonToTakeAction, HospitalEntryPersonToTakeAction>()
                .ForMember(dto => dto.HospitalEntry, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<HospitalEntryPersonToTakeAction, GetHospitalEntryPersonToTakeAction>()
                .ForMember(dto => dto.StaffName, mem => mem.Ignore());
            #endregion

            #region HospitalExit
            CreateMap<PutHospitalExit, HospitalExit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostHospitalExit, HospitalExit>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<HospitalExit, GetHospitalExit>();

            CreateMap<PutHospitalExitOfficerToTakeAction, HospitalExitOfficerToTakeAction>()
                .ForMember(dto => dto.HospitalExit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostHospitalExitOfficerToTakeAction, HospitalExitOfficerToTakeAction>()
                .ForMember(dto => dto.HospitalExit, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<HospitalExitOfficerToTakeAction, GetHospitalExitOfficerToTakeAction>()
                .ForMember(dto => dto.StaffName, mem => mem.Ignore());
            #endregion

            #region StaffPersonalityTest
            CreateMap<PutStaffPersonalityTest, StaffPersonalityTest>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostStaffPersonalityTest, StaffPersonalityTest>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<StaffPersonalityTest, GetStaffPersonalityTest>()
                .ForMember(dto => dto.QuestionName, mem => mem.Ignore())
                .ForMember(dto => dto.AnswerName, mem => mem.Ignore());
            #endregion

            #region HomeRiskAssessment
            CreateMap<PutHomeRiskAssessment, HomeRiskAssessment>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                 .ForMember(dto => dto.HomeRiskAssessmentTask, mem => mem.MapFrom(src => src.PutHomeRiskAssessmentTask));
            CreateMap<PostHomeRiskAssessment, HomeRiskAssessment>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                 .ForMember(dto => dto.HomeRiskAssessmentTask, mem => mem.MapFrom(src => src.PostHomeRiskAssessmentTask));
            CreateMap<HomeRiskAssessment, GetHomeRiskAssessment>()
                 .ForMember(dto => dto.GetHomeRiskAssessmentTask, mem => mem.Ignore());

            CreateMap<PostHomeRiskAssessmentTask, HomeRiskAssessmentTask>()
                .ForMember(dto => dto.HomeRiskAssessment, mem => mem.Ignore());
            CreateMap<PutHomeRiskAssessmentTask, HomeRiskAssessmentTask>()
                .ForMember(dto => dto.HomeRiskAssessment, mem => mem.Ignore());
            CreateMap<HomeRiskAssessmentTask, GetHomeRiskAssessmentTask>()
                .ForMember(dto => dto.AnswerName, mem => mem.Ignore())
                .ForMember(dto => dto.TitleName, mem => mem.Ignore());
            #endregion

            #region StaffInfectionControl
            CreateMap<PutStaffInfectionControl, StaffInfectionControl>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostStaffInfectionControl, StaffInfectionControl>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());

            CreateMap<StaffInfectionControl, GetStaffInfectionControl>();
            #endregion

            #region DutyOnCall
            CreateMap<PutDutyOnCall, DutyOnCall>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostDutyOnCall, DutyOnCall>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<DutyOnCall, GetDutyOnCall>();

            CreateMap<PutDutyOnCallPersonToAct, DutyOnCallPersonToAct>()
                .ForMember(dto => dto.DutyOnCall, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostDutyOnCallPersonToAct, DutyOnCallPersonToAct>()
                .ForMember(dto => dto.DutyOnCall, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<DutyOnCallPersonToAct, GetDutyOnCallPersonToAct>()
                .ForMember(dto => dto.StaffName, mem => mem.Ignore());

            CreateMap<PutDutyOnCallPersonResponsible, DutyOnCallPersonResponsible>()
                .ForMember(dto => dto.DutyOnCall, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostDutyOnCallPersonResponsible, DutyOnCallPersonResponsible>()
                .ForMember(dto => dto.DutyOnCall, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<DutyOnCallPersonResponsible, GetDutyOnCallPersonResponsible>()
                .ForMember(dto => dto.StaffName, mem => mem.Ignore());
            #endregion

            #region TrackingConcernNote
            CreateMap<PutTrackingConcernNote, TrackingConcernNote>();
            CreateMap<PostTrackingConcernNote, TrackingConcernNote>();
            CreateMap<TrackingConcernNote, GetTrackingConcernNote>();

            CreateMap<PutTrackingConcernManager, TrackingConcernManager>()
                .ForMember(dto => dto.TrackingConcernNote, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostTrackingConcernManager, TrackingConcernManager>()
                .ForMember(dto => dto.TrackingConcernNote, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<TrackingConcernManager, GetTrackingConcernManager>()
                .ForMember(dto => dto.StaffName, mem => mem.Ignore());

            CreateMap<PutTrackingConcernStaff, TrackingConcernStaff>()
                .ForMember(dto => dto.TrackingConcernNote, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostTrackingConcernStaff, TrackingConcernStaff>()
                .ForMember(dto => dto.TrackingConcernNote, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<TrackingConcernStaff, GetTrackingConcernStaff>()
                .ForMember(dto => dto.StaffName, mem => mem.Ignore());
            #endregion

            #region StaffCompetenceTest
            CreateMap<PutStaffCompetenceTest, StaffCompetenceTest>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                 .ForMember(dto => dto.StaffCompetenceTestTask, mem => mem.MapFrom(src => src.PutStaffCompetenceTestTask));
            CreateMap<PostStaffCompetenceTest, StaffCompetenceTest>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                 .ForMember(dto => dto.StaffCompetenceTestTask, mem => mem.MapFrom(src => src.PostStaffCompetenceTestTask));
            CreateMap<StaffCompetenceTest, GetStaffCompetenceTest>()
                 .ForMember(dto => dto.GetStaffCompetenceTestTask, mem => mem.Ignore());

            CreateMap<PostStaffCompetenceTestTask, StaffCompetenceTestTask>()
                .ForMember(dto => dto.StaffCompetenceTest, mem => mem.Ignore());
            CreateMap<PutStaffCompetenceTestTask, StaffCompetenceTestTask>()
                .ForMember(dto => dto.StaffCompetenceTest, mem => mem.Ignore());
            CreateMap<StaffCompetenceTestTask, GetStaffCompetenceTestTask>()
                .ForMember(dto => dto.AnswerName, mem => mem.Ignore())
                .ForMember(dto => dto.TitleName, mem => mem.Ignore());
            #endregion

            #region StaffHealth
            CreateMap<PutStaffHealth, StaffHealth>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                 .ForMember(dto => dto.StaffHealthTask, mem => mem.MapFrom(src => src.PutStaffHealthTask));
            CreateMap<PostStaffHealth, StaffHealth>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                 .ForMember(dto => dto.StaffHealthTask, mem => mem.MapFrom(src => src.PostStaffHealthTask));
            CreateMap<StaffHealth, GetStaffHealth>()
                 .ForMember(dto => dto.GetStaffHealthTask, mem => mem.Ignore());

            CreateMap<PostStaffHealthTask, StaffHealthTask>()
                .ForMember(dto => dto.StaffHealth, mem => mem.Ignore());
            CreateMap<PutStaffHealthTask, StaffHealthTask>()
                .ForMember(dto => dto.StaffHealth, mem => mem.Ignore());
            CreateMap<StaffHealthTask, GetStaffHealthTask>()
                .ForMember(dto => dto.AnswerName, mem => mem.Ignore())
                .ForMember(dto => dto.TitleName, mem => mem.Ignore());
            #endregion

            #region StaffInterview
            CreateMap<PutStaffInterview, StaffInterview>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                 .ForMember(dto => dto.StaffInterviewTask, mem => mem.MapFrom(src => src.PutStaffInterviewTask));
            CreateMap<PostStaffInterview, StaffInterview>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                 .ForMember(dto => dto.StaffInterviewTask, mem => mem.MapFrom(src => src.PostStaffInterviewTask));
            CreateMap<StaffInterview, GetStaffInterview>()
                 .ForMember(dto => dto.GetStaffInterviewTask, mem => mem.Ignore());

            CreateMap<PostStaffInterviewTask, StaffInterviewTask>()
                .ForMember(dto => dto.StaffInterview, mem => mem.Ignore());
            CreateMap<PutStaffInterviewTask, StaffInterviewTask>()
                .ForMember(dto => dto.StaffInterview, mem => mem.Ignore());
            CreateMap<StaffInterviewTask, GetStaffInterviewTask>()
                .ForMember(dto => dto.AnswerName, mem => mem.Ignore())
                .ForMember(dto => dto.TitleName, mem => mem.Ignore());
            #endregion

            #region StaffShadowing
            CreateMap<PutStaffShadowing, StaffShadowing>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                 .ForMember(dto => dto.StaffShadowingTask, mem => mem.MapFrom(src => src.PutStaffShadowingTask));
            CreateMap<PostStaffShadowing, StaffShadowing>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                 .ForMember(dto => dto.StaffShadowingTask, mem => mem.MapFrom(src => src.PostStaffShadowingTask));
            CreateMap<StaffShadowing, GetStaffShadowing>()
                 .ForMember(dto => dto.GetStaffShadowingTask, mem => mem.Ignore());

            CreateMap<PostStaffShadowingTask, StaffShadowingTask>()
                .ForMember(dto => dto.StaffShadowing, mem => mem.Ignore());
            CreateMap<PutStaffShadowingTask, StaffShadowingTask>()
                .ForMember(dto => dto.StaffShadowing, mem => mem.Ignore());
            CreateMap<StaffShadowingTask, GetStaffShadowingTask>()
                .ForMember(dto => dto.AnswerName, mem => mem.Ignore())
                .ForMember(dto => dto.TitleName, mem => mem.Ignore());
            #endregion

            #region PerformanceIndicator
            CreateMap<PostPerformanceIndicator, PerformanceIndicator>()
                 .ForMember(dto => dto.PerformanceIndicatorTask, mem => mem.MapFrom(src => src.PostPerformanceIndicatorTask));
            CreateMap<PerformanceIndicator, GetPerformanceIndicator>()
                 .ForMember(dto => dto.GetPerformanceIndicatorTask, mem => mem.Ignore());

            CreateMap<PostPerformanceIndicatorTask, PerformanceIndicatorTask>()
                .ForMember(dto => dto.PerformanceIndicator, mem => mem.Ignore());
            CreateMap<PerformanceIndicatorTask, GetPerformanceIndicatorTask>()
                .ForMember(dto => dto.TitleName, mem => mem.Ignore());
            #endregion

            #region DailyTask
            CreateMap<PutClientDailyTask, ClientDailyTask>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());
            CreateMap<PostClientDailyTask, ClientDailyTask>()
                .ForMember(dto => dto.Client, mem => mem.Ignore());

            CreateMap<ClientDailyTask, GetClientDailyTask>();
            #endregion

            #region StaffHoliday
            CreateMap<PutStaffHoliday, StaffHoliday>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostStaffHoliday, StaffHoliday>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());

            CreateMap<StaffHoliday, GetStaffHoliday>();
            #endregion

            #region SetupStaffHoliday
            CreateMap<PutSetupStaffHoliday, SetupStaffHoliday>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());
            CreateMap<PostSetupStaffHoliday, SetupStaffHoliday>()
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore());

            CreateMap<SetupStaffHoliday, GetSetupStaffHoliday>();
            #endregion

            #region StaffTeamLead
            CreateMap<PutStaffTeamLead, StaffTeamLead>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.StaffTeamLeadTasks, mem => mem.MapFrom(src => src.PutStaffTeamLeadTasks));
            CreateMap<PostStaffTeamLead, StaffTeamLead>()
                .ForMember(dto => dto.Client, mem => mem.Ignore())
                .ForMember(dto => dto.StaffPersonalInfo, mem => mem.Ignore())
                .ForMember(dto => dto.StaffTeamLeadTasks, mem => mem.MapFrom(src => src.PostStaffTeamLeadTasks));
            CreateMap<StaffTeamLead, GetStaffTeamLead>()
                .ForMember(dto => dto.GetStaffTeamLeadTasks, mem => mem.Ignore());
            #endregion

            #region StaffTeamLeadTasks
            CreateMap<PutStaffTeamLeadTasks, StaffTeamLeadTasks>()
                .ForMember(dto => dto.StaffTeamLead, mem => mem.Ignore());
            CreateMap<PostStaffTeamLeadTasks, StaffTeamLeadTasks>()
                .ForMember(dto => dto.StaffTeamLead, mem => mem.Ignore());
            CreateMap<StaffTeamLeadTasks, GetStaffTeamLeadTasks>();
            #endregion
        }
    }
}
