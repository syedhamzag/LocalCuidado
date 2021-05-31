using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs;
using Refit;

namespace AwesomeCare.Web.Services.IncidentReport
{
   public interface IIncidentReportService
    {
        [Get("/IncidentReporting/Staff/IncidentReport/BySignedInUser")]
        Task<List<GetStaffIncidentReport>> GetStaffReports();

        [Post("/IncidentReporting/Staff/IncidentReport")]
        Task<HttpResponseMessage> PostStaffReport([Body] PostReportStaff model);

        [Get("/IncidentReporting/Staff/IncidentReport/BySignedInUser/{id}")]
        Task<GetStaffIncidentReport> GetStaffReportById(int id);
    }
}
