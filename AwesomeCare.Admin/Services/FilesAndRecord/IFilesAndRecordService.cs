using AwesomeCare.DataTransferObject.DTOs.FilesAndRecord;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.FilesAndRecord
{
    public interface IFilesAndRecordService
    {
        [Get("/FilesAndRecord")]
        Task<List<GetFilesAndRecord>> Get();

        [Get("/FilesAndRecord/Get/{id}")]
        Task<GetFilesAndRecord> Get(int id);

        [Post("/FilesAndRecord/Create")]
        Task<HttpResponseMessage> Create([Body] PostFilesAndRecord model);

        [Put("/FilesAndRecord/Put")]
        Task<HttpResponseMessage> Put([Body] PutFilesAndRecord model);
    }
}
