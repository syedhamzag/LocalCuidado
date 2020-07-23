using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Web.ViewModels.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web
{
    public class MapperProfile : MapperConfig.MapperProfile
    {
        public MapperProfile()
        {
            CreateMap<CreateStaff, PostStaffFullInfo>()
                .ForMember(dto => dto.StaffTrainings, mem => mem.MapFrom(src => src.Trainings))
                .ForMember(dto => dto.StaffEducations, mem => mem.MapFrom(src => src.Education))
                .ForMember(dto => dto.StaffReferees, mem => mem.MapFrom(src => src.References))
                .ForMember(dto => dto.StaffRegulatoryContacts, mem => mem.MapFrom(src => src.RegulatoryContacts))
                .ForMember(dto => dto.StartDate, mem => mem.MapFrom(src => DateTime.Now))
                .ForMember(dto => dto.Gender, mem => mem.MapFrom(src => src.GenderId));

            CreateMap<CreateStaffEducation, PostStaffEducation>()
                .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.Ignore());

            CreateMap<CreateStaffTraining, PostStaffTraining>()
                .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.Ignore());

            CreateMap<CreateStaffReference, PostStaffReferee>()
                .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.Ignore());

            CreateMap<CreateStaffRegulatoryContact, PostStaffRegulatoryContact>()
                 .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.Ignore());

            CreateMap<CreateStaffEmergencyContact, PostStaffEmergencyContact>()
               .ForMember(dto => dto.StaffPersonalInfoId, mem => mem.Ignore());

            CreateMap<GetStaffPersonalInfo, UpdatePersonalInfo>()
                .ForMember(dto => dto.SelfPYE, mem => mem.MapFrom(src=>src.Self_PYE))
                .ForMember(dto => dto.SelfPYEAttachment, mem => mem.MapFrom(src=>src.Self_PYEAttachment))
                .ForMember(dto => dto.ProfilePixFile, mem => mem.Ignore())
                .ForMember(dto => dto.DrivingLicenseFile, mem => mem.Ignore())
                .ForMember(dto => dto.RightToWorkFile, mem => mem.Ignore())
                .ForMember(dto => dto.DbsFile, mem => mem.Ignore())
                .ForMember(dto => dto.NiFile, mem => mem.Ignore())
                .ForMember(dto => dto.SelfPyeFile, mem => mem.Ignore())
                .ForMember(dto => dto.CoverLetterFile, mem => mem.Ignore())
                .ForMember(dto => dto.TabIndex, mem => mem.Ignore())
                .ForMember(dto => dto.CvFile, mem => mem.Ignore());
        }
    }
}
