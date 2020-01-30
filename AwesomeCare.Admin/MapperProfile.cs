using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetails;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.Company;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
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
           // CreateMap<GetCompanyDto, UpdateCompanyDto>();
        }
    }
}
