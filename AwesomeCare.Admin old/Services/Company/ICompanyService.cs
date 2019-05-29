using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace AwesomeCare.Admin.Services.Company
{
   public interface ICompanyService
    {
        [Get("/api/Company/{companyId}")]
        Task<HttpResponseMessage> GetCompany(int companyId);
    }
}
