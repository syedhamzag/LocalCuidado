﻿using AwesomeCare.DataTransferObject.DTOs.StaffKeyWorker;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.StaffKeyWorkerVoice
{
    public interface IStaffKeyWorkerVoiceService
    {
        [Get("/StaffKeyWorkerVoice")]
        Task<List<GetStaffKeyWorkerVoice>> Get();

        [Get("/StaffKeyWorkerVoice/Get/{id}")]
        Task<GetStaffKeyWorkerVoice> Get(int id);

        [Post("/StaffKeyWorkerVoice/Create")]
        Task<HttpResponseMessage> Create([Body] PostStaffKeyWorkerVoice model);

        [Put("/StaffKeyWorkerVoice/Put")]
        Task<HttpResponseMessage> Put([Body] PutStaffKeyWorkerVoice model);
    }
}
