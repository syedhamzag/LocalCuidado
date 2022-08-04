using AwesomeCare.DataTransferObject.DTOs.OfficeAttendance;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.OfficeAttendance
{
    public interface IOfficeAttendanceService
    {
        [Get("/OfficeAttendance")]
        Task<List<GetAttendance>> Get();

        [Get("/OfficeAttendance/GetByStaff/{id}")]
        Task<List<GetAttendance>> GetByStaff(int id);

        [Get("/OfficeAttendance/GetByDate/{sdate}/{edate}")]
        Task<List<GetAttendance>> GetByDate(string sdate, string edate);

        [Get("/OfficeAttendance/Get/{id}")]
        Task<GetAttendance> Get(int id);

        [Post("/OfficeAttendance/Post")]
        Task<HttpResponseMessage> Post([Body] PostAttendance model);

        [Put("/OfficeAttendance/Put")]
        Task<HttpResponseMessage> Put([Body] PutAttendance model);
    }
}
