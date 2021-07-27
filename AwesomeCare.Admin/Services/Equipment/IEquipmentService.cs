using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Equipment
{
    public interface IEquipmentService
    {
        [Get("/Equipment")]
        Task<List<GetEquipment>> Get();

        [Get("/Equipment/Get/{id}")]
        Task<GetEquipment> Get(int id);

        [Post("/Equipment/Create")]
        Task<HttpResponseMessage> Create([Body] PostEquipment model);

        [Put("/Equipment/Put")]
        Task<HttpResponseMessage> Put([Body] PutEquipment model);
    }
}
