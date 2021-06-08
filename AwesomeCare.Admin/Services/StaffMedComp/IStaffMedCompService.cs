using AwesomeCare.DataTransferObject.DTOs.StaffMedComp;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffMedComp
{
    public interface IStaffMedCompService
    {
        [Get("/StaffMedComp")]
        Task<List<GetStaffMedComp>> Get();

        [Get("/StaffMedComp/Get/{id}")]
        Task<GetStaffMedComp> Get(int id);

        [Post("/StaffMedComp/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffMedComp model);

        [Put("/StaffMedComp")]
        Task<GetStaffMedComp> Put([Body] PutStaffMedComp model);
    }
}
