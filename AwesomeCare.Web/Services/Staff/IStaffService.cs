using AwesomeCare.DataTransferObject.DTOs.Staff;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Web.Services.Staff
{
  public  interface IStaffService
    {
        [Post("/StaffInfo/PostStaffFullInfo")]
        Task<HttpResponseMessage> PostStaffFullInfo([Body]PostStaffFullInfo model);

        [Get("/StaffInfo/Profile/{id}")]
        Task<GetStaffProfile> Profile(int id);
    }
}
