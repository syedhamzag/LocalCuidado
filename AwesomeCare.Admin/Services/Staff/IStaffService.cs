using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffRating;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using Refit;

namespace AwesomeCare.Admin.Services.Staff
{
    public interface IStaffService
    {
        [Get("/StaffInfo/GetStaffs")]
        Task<List<GetStaffs>> GetStaffs();

        [Get("/StaffInfo/{id}")]
        Task<GetStaffPersonalInfo> GetStaff(int id);


        [Get("/StaffInfo/Profile/{id}")]
        Task<StaffDetails> Profile(int id);

        [Post("/StaffInfo/Approval")]
        Task<HttpResponseMessage> Approval([Body]PostStaffApproval postStaffApproval);

        [Put("/StaffInfo/MyProfile/Edit")]
        Task<HttpResponseMessage> UpdateStaffPersonalProfile([Body] PutStaffPersonalInfo model);

        #region StaffRota
        [Post("/StaffInfo/Rota/Create")]
        Task<HttpResponseMessage> CreateRota([Body]List<PostStaffRota> postStaffRotas);

        [Post("/StaffInfo/Rota/Dynamic")]
        Task<HttpResponseMessage> CreateRotaSelection([Body]PostStaffRotaDynamicAddition model);

        [Put("/StaffInfo/Rota/Dynamic")]
        Task<HttpResponseMessage> UpdateRotaSelection([Body]PutStaffRotaDynamicAddition model);

        [Get("/StaffInfo/Rota/Dynamic")]
        Task<List<GetStaffRotaDynamicAddition>> GetRotaSelections();

        [Get("/StaffInfo/Rota/Dynamic/{id}")]
        Task<GetStaffRotaDynamicAddition> GetRotaSelection(int id);
        #endregion

        #region Client Feedback/Rating
        [Get("/StaffInfo/ClientFeedback/{staffPersonalInfoId}")]
        Task<List<GetStaffRating>> GetClientFeedback(int? staffPersonalInfoId);

        [Post("/StaffInfo/ClientFeedback")]
        Task<HttpResponseMessage> PostClientFeedback([Body]PostStaffRating model);
        #endregion
    }
}
