using AwesomeCare.DataTransferObject.DTOs.Company;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Company
{
    public interface ICompanyService
    {
        [Get("/Company/{companyId}")]
        Task<GetCompanyDto> GetCompany(int companyId);
        [Get("/Company")]
        Task<IEnumerable<GetCompanyDto>> GetCompanies();
        [Post("/Company")]
        Task<HttpResponseMessage> AddCompany(CreateCompanyDto companyDto);
        [Put("/Company/{companyId}")]
        Task<GetCompanyDto> UpdateCompany(UpdateCompanyDto companyDto, int companyId);
    }
}
