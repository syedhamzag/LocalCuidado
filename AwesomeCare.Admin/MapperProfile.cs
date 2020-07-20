using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.Admin.ViewModels.ShiftBooking;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.Admin.ViewModels.StaffCommunication;
using AwesomeCare.Admin.ViewModels.Untowards;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetails;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.ClientMedication;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
using AwesomeCare.DataTransferObject.DTOs.Company;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffCommunication;
using AwesomeCare.DataTransferObject.DTOs.Untowards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin
{
    public class MapperProfile : MapperConfig.MapperProfile
    {
        public MapperProfile()
        {
            CreateMap<CreateClient, PostClient>()
                .ForMember(dto => dto.CareDetails, mem => mem.Ignore());

            CreateMap<ClientInvolvingParty, PostClientInvolvingParty>()
                .ForMember(dto=>dto.ClientId,mem=>mem.Ignore());
            CreateMap<ClientRegulatoryContact, PostClientRegulatoryContact>()
                  .ForMember(dto => dto.ClientId, mem => mem.Ignore());
            CreateMap<ClientCareDetailsTask, PostClientCareDetails>()
                .ForMember(dto => dto.ClientCareDetailsTaskId, mem => mem.MapFrom(src=>src.CareDetailsTaskId))
                .ForMember(dto => dto.Description, mem => mem.MapFrom(src=>src.Description))
                .ForMember(dto => dto.Risk, mem => mem.MapFrom(src=>src.Risk))
                .ForMember(dto => dto.Mitigation, mem => mem.MapFrom(src=>src.Mitigation))
                .ForMember(dto => dto.Location, mem => mem.MapFrom(src=>src.Location))
                .ForMember(dto => dto.Remark, mem => mem.MapFrom(src=>src.Remark))
                .ForMember(dto => dto.ClientId, mem => mem.Ignore());

            CreateMap<CreateStaffCommunication, PostStaffCommunication>();
            // CreateMap<GetCompanyDto, UpdateCompanyDto>();

            CreateMap<CreateUntowards, PostUntowards>();
          
            CreateMap<CreateShiftBooking, PostShiftBooking>();

            CreateMap<CreateMedicationViewModel, PostClientMedication>();
            CreateMap<CreateMedicationDay, PostClientMedicationDay>();
            CreateMap<CreateMedicationPeriod, PostClientMedicationPeriod>();

            CreateMap<GetStaffPersonalInfo, UpdateStaffPersonalInfo>()
              .ForMember(dto => dto.SelfPYE, mem => mem.MapFrom(src => src.Self_PYE))
              .ForMember(dto => dto.SelfPYEAttachment, mem => mem.MapFrom(src => src.Self_PYEAttachment))
              .ForMember(dto => dto.ProfilePixFile, mem => mem.Ignore())
              .ForMember(dto => dto.DrivingLicenseFile, mem => mem.Ignore())
              .ForMember(dto => dto.RightToWorkFile, mem => mem.Ignore())
              .ForMember(dto => dto.DbsFile, mem => mem.Ignore())
              .ForMember(dto => dto.NiFile, mem => mem.Ignore())
              .ForMember(dto => dto.SelfPyeFile, mem => mem.Ignore())
              .ForMember(dto => dto.CoverLetterFile, mem => mem.Ignore())
              .ForMember(dto => dto.CvFile, mem => mem.Ignore());
        }
    }
}
