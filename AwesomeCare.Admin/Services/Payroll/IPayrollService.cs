using AwesomeCare.DataTransferObject.DTOs.Staff;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Payroll
{
    public interface IPayrollService
    {
        [Get("/Payroll")]
        Task<List<GetStaffProfile>> Get();

        [Get("/Payroll/Get/{id}")]
        Task<GetStaffProfile> Get(int id);

        //[Post("/Payroll/Create")]
        //Task<HttpResponseMessage> Create([Body] PostPayroll model);

        //[Put("/Payroll/Put")]
        //Task<HttpResponseMessage> Put([Body] PostPayroll model);
    }
}
