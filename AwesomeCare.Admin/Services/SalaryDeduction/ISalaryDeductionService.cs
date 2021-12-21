using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryDeduction;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.SalaryDeduction
{
    public interface ISalaryDeductionService
    {
        [Get("/SalaryDeduction")]
        Task<List<GetSalaryDeduction>> Get();

        [Get("/SalaryDeduction/Get/{id}")]
        Task<GetSalaryDeduction> Get(int id);

        [Post("/SalaryDeduction/Create")]
        Task<HttpResponseMessage> Create([Body] PostSalaryDeduction model);

        [Put("/SalaryDeduction/Put")]
        Task<HttpResponseMessage> Put([Body] PutSalaryDeduction model);
    }
}
