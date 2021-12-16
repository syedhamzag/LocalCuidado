using AwesomeCare.DataTransferObject.DTOs.StaffTrainingMatrix;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Staff
{
    public interface IStaffTrainingMatrixService
    {
        [Get("/StaffTrainingMatrix")]
        Task<List<GetStaffTrainingMatrix>> Get();

        [Get("/StaffTrainingMatrix/Get/{id}")]
        Task<GetStaffTrainingMatrix> Get(int id);

        [Post("/StaffTrainingMatrix/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffTrainingMatrix model);

        [Put("/StaffTrainingMatrix/Put")]
        Task<HttpResponseMessage> Put([Body] PutStaffTrainingMatrix model);
    }
}
