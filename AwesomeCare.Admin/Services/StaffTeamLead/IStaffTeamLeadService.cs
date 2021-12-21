using AwesomeCare.DataTransferObject.DTOs.StaffTeamLead;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffTeamLead
{
    public interface IStaffTeamLeadService
    {
        [Get("/StaffTeamLead")]
        Task<List<GetStaffTeamLead>> Get();

        [Get("/StaffTeamLead/Get/{id}")]
        Task<GetStaffTeamLead> Get(int id);

        [Post("/StaffTeamLead/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffTeamLead model);

        [Put("/StaffTeamLead/Put")]
        Task<HttpResponseMessage> Put([Body] PostStaffTeamLead model);
    }
}
