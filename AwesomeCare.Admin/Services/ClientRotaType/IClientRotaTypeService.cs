using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientRotaType
{
    public interface IClientRotaTypeService
    {

        [Get("/ClientRotaType")]
        Task<List<GetClientRotaType>> Get();

        [Post("/ClientRotaType")]
        Task<GetClientRotaType> Post([Body]PostClientRotaType model);

        [Get("/ClientRotaType/{id}")]
        Task<GetClientRotaType> Get(int id);

        [Put("/ClientRotaType")]
        Task<GetClientRotaType> Put([Body]PutClientRotaType model);
    }
}
