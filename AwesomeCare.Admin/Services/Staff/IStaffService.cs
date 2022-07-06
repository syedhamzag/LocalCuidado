using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffRating;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using AwesomeCare.DataTransferObject.DTOs.User;
using Refit;

namespace AwesomeCare.Admin.Services.Staff
{
    public interface IStaffService
    {
        [Get("/StaffInfo/GetStaffs")]
        Task<List<GetStaffs>> GetStaffs();

        [Get("/StaffInfo/")]
        Task<List<GetStaffPersonalInfo>> GetAsync();

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

        [Post("/StaffInfo/MedicationRota/Create")]
        Task<HttpResponseMessage> CreateMedRota([Body] List<PostStaffMedRota> postStaffRotas);

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

        [Get("/StaffInfo/ClientFeedback")]
        Task<List<GetStaffRating>> GetClientFeedback();

        [Post("/StaffInfo/ClientFeedback")]
        Task<HttpResponseMessage> PostClientFeedback([Body]PostStaffRating model);
        #endregion

        #region ChangeEmail
        [Get("/User/ChangeEmail/{userId}")]
        Task<GetChangeEmail> GetChangeEmail(string userId);

        [Post("/User/ChangeEmail")]
        Task<HttpResponseMessage> PostChangeEmail(PostChangeEmail model);

        [Post("/User/Admin/ResetPassord")]
        Task<HttpResponseMessage> ResetUserPassword(PostResetPassord model);
        #endregion

        #region Staff Details
        [Get("/StaffInfo/StaffHoliday/{id}")]
        Task<StaffDetails> StaffHoliday(int id);

        [Get("/StaffInfo/StaffEducation/{id}")]
        Task<StaffDetails> StaffEducation(int id);
        
        [Get("/StaffInfo/StaffTrainingMatrix/{id}")]
        Task<StaffDetails> StaffTrainingMatrix(int id);
        
        [Get("/StaffInfo/StaffTraining/{id}")]
        Task<StaffDetails> StaffTraining(int id);
        
        [Get("/StaffInfo/StaffReferee/{id}")]
        Task<StaffDetails> StaffReferee(int id);
        
        [Get("/StaffInfo/StaffEmergencyContact/{id}")]
        Task<StaffDetails> StaffEmergencyContact(int id);
        
        [Get("/StaffInfo/StaffSpotCheck/{id}")]
        Task<StaffDetails> StaffSpotCheck(int id);

        [Get("/StaffInfo/StaffAdlObs/{id}")]
        Task<StaffDetails> StaffAdlObs(int id);
        
        [Get("/StaffInfo/StaffMedComp/{id}")]
        Task<StaffDetails> StaffMedComp(int id);
        
        [Get("/StaffInfo/StaffKeyWorker/{id}")]
        Task<StaffDetails> StaffKeyWorker(int id);
        
        [Get("/StaffInfo/StaffSurvey/{id}")]
        Task<StaffDetails> StaffSurvey(int id);
        
        [Get("/StaffInfo/StaffOneToOne/{id}")]
        Task<StaffDetails> StaffOneToOne(int id);

        [Get("/StaffInfo/StaffSupervision/{id}")]
        Task<StaffDetails> StaffSupervision(int id);

        [Get("/StaffInfo/StaffReference/{id}")]
        Task<StaffDetails> StaffReference(int id);

        [Get("/StaffInfo/StaffPersonalityTest/{id}")]
        Task<StaffDetails> StaffPersonalityTest(int id);

        [Get("/StaffInfo/StaffInfectionControl/{id}")]
        Task<StaffDetails> StaffInfectionControl(int id);

        [Get("/StaffInfo/StaffCompetenceTest/{id}")]
        Task<StaffDetails> StaffCompetenceTest(int id);

        [Get("/StaffInfo/StaffHealth/{id}")]
        Task<StaffDetails> StaffHealth(int id);

        [Get("/StaffInfo/StaffInterview/{id}")]
        Task<StaffDetails> StaffInterview(int id);

        [Get("/StaffInfo/StaffShadowing/{id}")]
        Task<StaffDetails> StaffShadowing(int id);

        [Get("/StaffInfo/StaffAllowance/{id}")]
        Task<StaffDetails> StaffAllowance(int id);

        [Get("/StaffInfo/StaffDeduction/{id}")]
        Task<StaffDetails> StaffDeduction(int id);

        [Get("/StaffInfo/StaffTeamLead/{id}")]
        Task<StaffDetails> StaffTeamLead(int id);

        

        #endregion
    }
}
