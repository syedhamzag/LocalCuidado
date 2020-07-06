using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs;
using Refit;

namespace AwesomeCare.Admin.Services.IncidentReport
{
   public interface IIncidentReportService
    {
        [Get("/IncidentReporting/Staff/IncidentReport")]
        Task<List<GetStaffIncidentReport>> GetStaffReports();

        [Post("/IncidentReporting/Staff/IncidentReport")]
        Task<HttpResponseMessage> PostStaffReport([Body] PostReportStaff model);

        [Get("/IncidentReporting/Staff/IncidentReport/{id}")]
        Task<GetStaffIncidentReport> GetStaffReportById(int id);
    }
}
