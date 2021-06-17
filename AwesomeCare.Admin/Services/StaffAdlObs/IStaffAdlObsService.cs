using AwesomeCare.DataTransferObject.DTOs.StaffAdlObs;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffAdlObs
{
    public interface IStaffAdlObsService
    {
        [Get("/StaffAdlObs/GetByRef/{Reference}")]
        Task<List<GetStaffAdlObs>> GetByRef(string Reference);

        [Get("/StaffAdlObs")]
        Task<List<GetStaffAdlObs>> Get();

        [Get("/StaffAdlObs/Get/{id}")]
        Task<GetStaffAdlObs> Get(int id);

        [Post("/StaffAdlObs/Create")]
        Task<HttpResponseMessage> Create([Body] List<PostStaffAdlObs> model);

        [Put("/StaffAdlObs/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutStaffAdlObs> model);
    }
}
