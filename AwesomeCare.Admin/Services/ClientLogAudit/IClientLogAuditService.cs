using AwesomeCare.DataTransferObject.DTOs.ClientLogAudit;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientLogAudit
{
    public interface IClientLogAuditService
    {
        [Get("/ClientLogAudit")]
        Task<List<GetClientLogAudit>> Get();

        [Get("/ClientLogAudit/Get/{id}")]
        Task<GetClientLogAudit> Get(int id);

        [Post("/ClientLogAudit/Create")]
        Task<HttpResponseMessage> Create([Body] PostClientLogAudit model);

        [Put("/ClientLogAudit")]
        Task<GetClientLogAudit> Put([Body] PutClientLogAudit model);
    }
}
