using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using Refit;

namespace AwesomeCare.Admin.Services.Staff
{
    public interface IStaffService
    {
        [Get("/StaffInfo/GetStaffs")]
        Task<List<GetStaffs>> GetStaffs();

        [Get("/StaffInfo/Profile/{id}")]
        Task<StaffDetails> Profile(int id);

        [Post("/StaffInfo/Approval")]
        Task<HttpResponseMessage> Approval([Body]PostStaffApproval postStaffApproval);
        
    }
}
