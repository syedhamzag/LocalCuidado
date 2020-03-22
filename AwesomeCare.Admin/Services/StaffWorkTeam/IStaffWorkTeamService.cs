using AwesomeCare.DataTransferObject.DTOs.StaffWorkTeam;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffWorkTeam
{
   public interface IStaffWorkTeamService
    {
        [Post("/StaffWorkTeam")]
        Task<HttpResponseMessage> Post([Body]PostStaffWorkTeam shiftBooking);
        [Get("/StaffWorkTeam")]
        Task<List<GetStaffWorkTeam>> Get();
        [Get("/StaffWorkTeam/{id}")]
        Task<GetStaffWorkTeam> Get(int id);
    }
}
