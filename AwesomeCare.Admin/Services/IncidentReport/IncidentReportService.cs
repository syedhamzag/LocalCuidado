using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs;
using AwesomeCare.DataTransferObject.DTOs.IncidentReporting;
using Refit;

namespace AwesomeCare.Admin.Services.IncidentReport
{
   public interface IncidentReportService
    {
        [Get("/IncidentReporting/Staff/IncidentReport")]
        Task<List<GetStaffIncidentReport>> GetStaffReports();

        [Post("/IncidentReporting/Staff/IncidentReport")]
        Task<HttpResponseMessage> PostStaffReport([Body] PostReportStaff model);

        [Get("/IncidentReporting/Staff/IncidentReport/{id}")]
        Task<GetStaffIncidentReport> GetStaffReportById(int id);

        [Get("/ClientIncident")]
        Task<List<GetIncidentReport>> Get();

        [Post("/ClientIncident/Post")]
        Task<HttpResponseMessage> Post([Body] PostIncidentReport model);

        [Post("/ClientIncident/Put")]
        Task<HttpResponseMessage> Put([Body] PutIncidentReport model);

        [Get("/ClientIncident/Get/{id}")]
        Task<GetIncidentReport> Get(int id);
    }
}
