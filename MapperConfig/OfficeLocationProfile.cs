using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.OfficeLocation;
using AwesomeCare.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapperConfig
{
   public class OfficeLocationProfile:Profile
    {
        public OfficeLocationProfile()
        {
            CreateMap<PostOfficeLocation, OfficeLocation>()
               .ForMember(dto => dto.OfficeLocationId, mem => mem.Ignore())
               .ForMember(dto => dto.UniqueId, mem => mem.Ignore());

            CreateMap<PutOfficeLocation, OfficeLocation>();

            CreateMap<OfficeLocation, GetOfficeLocation>();
        }
    }
}
