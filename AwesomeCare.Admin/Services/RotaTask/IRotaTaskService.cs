using AwesomeCare.DataTransferObject.DTOs.ClientRotaTask;
using AwesomeCare.DataTransferObject.DTOs.RotaTask;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using AwesomeCare.DataTransferObject.DTOs.StaffRotaPeriod;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.RotaTask
{
   public interface IRotaTaskService
    {
        [Get("/RotaTask")]
        Task<List<GetRotaTask>> Get();

        [Post("/RotaTask")]
        Task<GetRotaTask> Post([Body]PostRotaTask model);

        [Get("/RotaTask/{id}")]
        Task<GetRotaTask> Get(int id);

        [Put("/RotaTask")]
        Task<GetRotaTask> Put([Body]PutRotaTask model);

        [Get("/Rotering/RotaAdmin/{sDate}/{eDate}")]
        Task<List<RotaAdmin>> RotaAdmin(string sDate, string eDate);

        [Get("/Rotering/LiveRota2/{startDate}/{stopDate}")]
        Task<List<LiveTracker>> LiveRota(string startDate,string stopDate);

        [Delete("/Rotering/DeleteStaffRotaPeriod/{staffRotaPeriodId}")]
        Task<HttpResponseMessage> DeleteStaffRotaPeriod(int staffRotaPeriodId);

        [Get("/Rotering/GetStaffRotaPeriodById/{staffRotaPeriodId}")]
        Task<GetStaffRotaPeriodForEdit> GetStaffRotaPeriod(int staffRotaPeriodId);

        [Put("/Rotering/PatchStaffRotaPeriod")]
        Task<HttpResponseMessage> PatchStaffRotaPeriod([Body] EditStaffRotaPeriod model);
    }
}
