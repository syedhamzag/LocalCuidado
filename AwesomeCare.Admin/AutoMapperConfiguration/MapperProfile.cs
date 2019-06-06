using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.AutoMapperConfiguration
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<GetCompanyDto, UpdateCompanyDto>();
        }
    }
}
