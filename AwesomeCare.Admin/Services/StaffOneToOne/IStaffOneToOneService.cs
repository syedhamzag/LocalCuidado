using AwesomeCare.DataTransferObject.DTOs.StaffOneToOne;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffOneToOne
{
    public interface IStaffOneToOneService
    {
        [Get("/StaffOneToOne/GetByRef/{Reference}")]
        Task<List<GetStaffOneToOne>> GetByRef(string Reference);

        [Get("/StaffOneToOne")]
        Task<List<GetStaffOneToOne>> Get();

        [Get("/StaffOneToOne/Get/{id}")]
        Task<GetStaffOneToOne> Get(int id);

        [Post("/StaffOneToOne/Create")]
        Task<HttpResponseMessage> Create([Body] List<PostStaffOneToOne> model);

        [Put("/StaffOneToOne/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutStaffOneToOne> model);
    }
}
