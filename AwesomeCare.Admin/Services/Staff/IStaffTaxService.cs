using AwesomeCare.DataTransferObject.DTOs.Staff.StaffTax;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Staff
{
    public interface IStaffTaxService
    {
        [Get("/StaffTax")]
        Task<List<GetStaffTax>> Get();

        [Get("/StaffTax/Get/{id}")]
        Task<GetStaffTax> Get(int id);

        [Post("/StaffTax/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffTax model);

        [Put("/StaffTax/Put")]
        Task<HttpResponseMessage> Put([Body] PutStaffTax model);
    }
}
