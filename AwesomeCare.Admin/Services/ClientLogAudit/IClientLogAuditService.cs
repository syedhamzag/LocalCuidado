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

        [Get("/ClientLogAudit/GetByRef/{Reference}")]
        Task<List<GetClientLogAudit>> GetByRef(string Reference);

        [Get("/ClientLogAudit/Get/{id}")]
        Task<GetClientLogAudit> Get(int id);

        [Post("/ClientLogAudit/Create")]
        Task<HttpResponseMessage> Create([Body] List<PostClientLogAudit> model);

        [Put("/ClientLogAudit/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutClientLogAudit> model);
    }
}
