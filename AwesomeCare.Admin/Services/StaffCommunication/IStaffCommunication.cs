using AwesomeCare.DataTransferObject.DTOs.StaffCommunication;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffCommunication
{
   public interface IStaffCommunication
    {
        [Get("/StaffCommunication")]
        Task<List<GetStaffCommunication>> GetStaffCommunication();

        [Get("/StaffCommunication/{id}")]
        Task<GetStaffCommunication> GetStaffCommunication(int id);

        [Post("/StaffCommunication")]
        Task<HttpResponseMessage> Post([Body]PostStaffCommunication postStaffApproval);
    }
}
