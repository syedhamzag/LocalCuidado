using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingPartyBase;
using AwesomeCare.DataTransferObject.DTOs.ClientMedication;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Client
{
   public interface IClientService
    {

        [Get("/Client")]
        Task<List<GetClient>> GetClients();

        [Get("/Client/GetClientDetails")]
        Task<List<GetClientDetail>> GetClientDetail();

        [Get("/Client/GetClient/{id}")]
        Task<GetClient> GetClient(int id);

        [Get("/Client/{clientId}")]
        Task<GetClientForEdit> GetClientForEdit(int clientId);

        [Post("/Client")]
        Task<GetClient> PostClient([Body]PostClient client);

        [Put("/Client/{clientId}")]
        Task<int> PutClient([Body]PutClient client,int clientId);

        [Get("/ClientInvolvingPartyBase")]
        Task<List<GetClientInvolvingPartyItem>> GetClientInvolvingPartyBase();

        [Get("/ClientInvolvingPartyBase/{id}")]
        Task<GetClientInvolvingPartyItem> GetClientInvolvingPartyBase(int id);

        #region ClientMedication
        [Get("/Client/Medication/{clientId}")]
        Task<List<GetClientMedication>> GetMedications(int clientId);

        [Post("/Client/Medication")]
        Task<HttpResponseMessage> PostMedication([Body]PostClientMedication model);

        [Get("/Client/Medication/{clientId}/{id}")]
        Task<GetClientMedication> GetMedication( int clientId, int id);

        [Put("/Client/Medication")]
        Task<HttpResponseMessage> PutMedication([Body]PutClientMedication model);
        #endregion

        #region Client Details
        [Get("/Client/GetHealthHobby/{id}")]
        Task<GetClient> GetHealthHobby(int id);

        [Get("/Client/GetInvolvingParty/{id}")]
        Task<GetClient> GetInvolvingParty(int id);

        [Get("/Client/GetComplain/{id}")]
        Task<GetClient> GetComplain(int id);

        [Get("/Client/GetLogAudit/{id}")]
        Task<GetClient> GetLogAudit(int id);

        [Get("/Client/GetMedAudit/{id}")]
        Task<GetClient> GetMedAudit(int id);

        [Get("/Client/GetVoice/{id}")]
        Task<GetClient> GetVoice(int id);

        [Get("/Client/GetMgtVisit/{id}")]
        Task<GetClient> GetMgtVisit(int id);

        [Get("/Client/GetProgram/{id}")]
        Task<GetClient> GetProgram(int id);

        [Get("/Client/GetServiceWatch/{id}")]
        Task<GetClient> GetServiceWatch(int id);

        [Get("/Client/GetBloodCoag/{id}")]
        Task<GetClient> GetBloodCoag(int id);

        [Get("/Client/GetPressure/{id}")]
        Task<GetClient> GetPressure(int id);

        [Get("/Client/GetBMIChart/{id}")]
        Task<GetClient> GetBMIChart(int id);

        [Get("/Client/GetBodyTemp/{id}")]
        Task<GetClient> GetBodyTemp(int id);

        [Get("/Client/GetBowel/{id}")]
        Task<GetClient> GetBowel(int id);

        [Get("/Client/GetEyeHealth/{id}")]
        Task<GetClient> GetEyeHealth(int id);

        [Get("/Client/GetFoodIntake/{id}")]
        Task<GetClient> GetFoodIntake(int id);

        [Get("/Client/GetHeartRate/{id}")]
        Task<GetClient> GetHeartRate(int id);

        [Get("/Client/GetOxygenLvl/{id}")]
        Task<GetClient> GetOxygenLvl(int id);

        [Get("/Client/GetPainChart/{id}")]
        Task<GetClient> GetPainChart(int id);

        [Get("/Client/GetPulseRate/{id}")]
        Task<GetClient> GetPulseRate(int id);

        [Get("/Client/GetSeizure/{id}")]
        Task<GetClient> GetSeizure(int id);

        [Get("/Client/GetWoundCare/{id}")]
        Task<GetClient> GetWoundCare(int id);

        [Get("/Client/GetReview/{id}")]
        Task<GetClient> GetReview(int id);

        [Get("/Client/GetPets/{id}")]
        Task<GetClient> GetPets(int id);

        [Get("/Client/GetInterestAndObj/{id}")]
        Task<GetClient> GetInterestAndObj(int id);

        [Get("/Client/GetPersonalHyg/{id}")]
        Task<GetClient> GetPersonalHyg(int id);

        [Get("/Client/GetInfectionControl/{id}")]
        Task<GetClient> GetInfectionControl(int id);

        [Get("/Client/GetManagingTask/{id}")]
        Task<GetClient> GetManagingTask(int id);

        [Get("/Client/GetCarePlanNut/{id}")]
        Task<GetClient> GetCarePlanNut(int id);

        [Get("/Client/GetBalance/{id}")]
        Task<GetClient> GetBalance(int id);

        [Get("/Client/GetPhysicalAbility/{id}")]
        Task<GetClient> GetPhysicalAbility(int id);

        [Get("/Client/GetHealthAndLiving/{id}")]
        Task<GetClient> GetHealthAndLiving(int id);

        [Get("/Client/GetHealthAndMed/{id}")]
        Task<GetClient> GetHealthAndMed(int id);

        [Get("/Client/GetHealthCondition/{id}")]
        Task<GetClient> GetHealthCondition(int id);

        [Get("/Client/GetHistoryOfFall/{id}")]
        Task<GetClient> GetHistoryOfFall(int id);

        [Get("/Client/GetHospitalEntry/{id}")]
        Task<GetClient> GetHospitalEntry(int id);

        [Get("/Client/GetHospitalExit/{id}")]
        Task<GetClient> GetHospitalExit(int id);

        [Get("/Client/GetHomeRisk/{id}")]
        Task<GetClient> GetHomeRisk(int id);

        [Get("/Client/GetDutyOnCall/{id}")]
        Task<GetClient> GetDutyOnCall(int id);

        [Get("/Client/GetDailyTask/{id}")]
        Task<GetClient> GetDailyTask(int id);

        [Get("/Client/GetBestInterest/{id}")]
        Task<GetClient> GetBestInterest(int id);

        [Get("/Client/GetFilesAndRecord/{id}")]
        Task<GetClient> GetFilesAndRecord(int id);

        [Get("/Client/GetCareObj/{id}")]
        Task<GetClient> GetCarObj(int id);

        [Get("/Client/GetClientDetails/{id}")]
        Task<GetClient> GetClientDetails(int id);
        #endregion
    }
}
