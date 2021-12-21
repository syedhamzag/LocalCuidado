using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryAllowance;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.SalaryAllowance
{
    public interface ISalaryAllowanceService
    {
        [Get("/SalaryAllowance")]
        Task<List<GetSalaryAllowance>> Get();

        [Get("/SalaryAllowance/Get/{id}")]
        Task<GetSalaryAllowance> Get(int id);

        [Post("/SalaryAllowance/Create")]
        Task<HttpResponseMessage> Create([Body] PostSalaryAllowance model);

        [Put("/SalaryAllowance/Put")]
        Task<HttpResponseMessage> Put([Body] PutSalaryAllowance model);
    }
}
