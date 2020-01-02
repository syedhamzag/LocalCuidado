using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetails;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading;
using Refit;

namespace AwesomeCare.Admin.Services.ClientCareDetails
{
   public interface IClientCareDetails
    {
        [Get("/ClientCareDetailsHeading/{id}")]
        Task<GetClientCareDetailsHeading> Get(int id);

        [Get("/ClientCareDetailsHeading")]
        Task<List<GetClientCareDetailsHeading>> GetAll();

        [Get("/ClientCareDetailsHeading/GetHeadingwithTasks/{id}")]
        Task<GetClientCareDetailsHeadingWithTasks> GetHeadingWithTasks(int id);

        [Get("/ClientCareDetailsHeading/GetHeadingsWithTasks")]
        Task<List<GetClientCareDetailsHeadingWithTasks>> GetHeadingsWithTasks();

        [Post("/ClientCareDetailsHeading")]
        Task<GetClientCareDetailsHeading> Post([Body]PostClientCareDetailsHeading model);

        [Post("/ClientCareDetailsHeading/PostHeadingwithTasks")]
        Task<HttpResponseMessage> PostWithTasks([Body]List<PostClientCareDetailsHeadingWithTasks> models);

        #region ClientCareDetails 
        [Post("/ClientCareDetails")]
        Task<HttpResponseMessage> PostClientDetails([Body]PostClientCareDetails model);
        [Post("/ClientCareDetails/PostClientCareDetails")]
        Task<HttpResponseMessage> PostClientDetails([Body]List<PostClientCareDetails> model);
        [Get("/ClientCareDetails/{id}")]
        Task<GetClientCareDetails> GetClientDetailsById(int id);
        [Get("/ClientCareDetails")]
        Task<GetClientCareDetails> GetClientDetails();
        #endregion
    }
}
