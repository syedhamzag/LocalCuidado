using AwesomeCare.DataTransferObject.DTOs.TrackingConcernNote;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.TrackingConcernNote
{
    public interface ITrackingConcernNote
    {
        [Get("/TrackingConcernNote")]
        Task<List<GetTrackingConcernNote>> Get();

        [Get("/TrackingConcernNote/GetWithChild")]
        Task<List<GetTrackingConcernNote>> GetWithChild();

        [Get("/TrackingConcernNote/Get/{id}")]
        Task<GetTrackingConcernNote> Get(int id);

        [Post("/TrackingConcernNote/Create")]
        Task<HttpResponseMessage> Create([Body] PostTrackingConcernNote model);

        [Put("/TrackingConcernNote/Put")]
        Task<HttpResponseMessage> Put([Body] PutTrackingConcernNote model);
    }
}
