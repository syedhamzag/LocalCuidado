using AwesomeCare.DataTransferObject.DTOs.Company;
using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Company
{
    public interface ICompanyContactService
    {
        [Get("/Company/{companyId}/CompanyContact")]
        Task<GetCompanyContactDto> GetCompanyContact(int companyId);       
        [Post("/Company/{companyId}/CompanyContact")]
        Task<HttpResponseMessage> AddCompanyContact(PostCompanyContactDto companyContactDto, int companyId);
        [Put("/Company/{companyId}/CompanyContact")]
        Task<GetCompanyDto> UpdateCompanyContact(UpdateCompanyDto companyDto, int companyId);
    }
}
