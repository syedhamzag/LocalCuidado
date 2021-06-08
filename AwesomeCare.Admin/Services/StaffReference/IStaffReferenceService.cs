using AwesomeCare.DataTransferObject.DTOs.StaffReference;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffReference
{
    public interface IStaffReferenceService
    {
        [Get("/StaffReference")]
        Task<List<GetStaffReference>> Get();

        [Get("/StaffReference/Get/{id}")]
        Task<GetStaffReference> Get(int id);

        [Post("/StaffReference/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffReference model);

        [Put("/StaffReference")]
        Task<GetStaffReference> Put([Body] PutStaffReference model);
    }
}
