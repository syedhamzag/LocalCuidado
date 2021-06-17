using AwesomeCare.DataTransferObject.DTOs.StaffSupervisionAppraisal;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffSupervisionAppraisal
{
    public interface IStaffSupervisionAppraisalService
    {
        [Get("/StaffSupervisionAppraisal/GetByRef/{Reference}")]
        Task<List<GetStaffSupervisionAppraisal>> GetByRef(string Reference);

        [Get("/StaffSupervisionAppraisal")]
        Task<List<GetStaffSupervisionAppraisal>> Get();

        [Get("/StaffSupervisionAppraisal/Get/{id}")]
        Task<GetStaffSupervisionAppraisal> Get(int id);

        [Post("/StaffSupervisionAppraisal/Create")]
        Task<HttpResponseMessage> Create([Body] List<PostStaffSupervisionAppraisal> model);

        [Put("/StaffSupervisionAppraisal/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutStaffSupervisionAppraisal> model);
    }
}
