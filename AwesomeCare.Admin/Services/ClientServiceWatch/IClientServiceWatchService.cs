﻿using AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.ClientServiceWatch
{
    public interface IClientServiceWatchService
    {
        [Get("/ClientServiceWatch/GetByRef/{Reference}")]
        Task<List<GetClientServiceWatch>> GetByRef(string Reference);

        [Get("/ClientServiceWatch")]
        Task<List<GetClientServiceWatch>> Get();

        [Get("/ClientServiceWatch/Get/{id}")]
        Task<GetClientServiceWatch> Get(int id);

        [Post("/ClientServiceWatch/Create")]
        Task<HttpResponseMessage> Create([Body] List<PostClientServiceWatch> model);

        [Put("/ClientServiceWatch/Put")]
        Task<HttpResponseMessage> Put([Body] List<PutClientServiceWatch> model);
    }
}
