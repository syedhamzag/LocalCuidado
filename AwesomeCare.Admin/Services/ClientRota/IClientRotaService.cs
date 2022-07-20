using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientRota
{
   public interface IClientRotaService
    {
        [Post("/ClientRota/CreateRota")]
        Task<HttpResponseMessage> CreateRota([Body]List<CreateClientRota> model);

        [Post("/ClientRota/ChangePin")]
        Task<HttpResponseMessage> ChangePin([Body] PostRotaPin model);

        [Post("/ClientRota/AddPin")]
        Task<HttpResponseMessage> AddPin([Body] PostRotaPin model);

        [Get("/ClientRota/GetPin/{id}")]
        Task<GetRotaPin> GetPin(int id);

        [Get("/ClientRota/GetPin")]
        Task<List<GetRotaPin>> GetPin();

        [Get("/ClientRota/GetForEdit/{id}")]
        Task<List<GetClientRota>> GetForEdit(int id);

        [Put("/ClientRota/Edit/{id}")]
        Task<HttpResponseMessage> EditRota([Body]List<CreateClientRota> model, int id);
    }
}
