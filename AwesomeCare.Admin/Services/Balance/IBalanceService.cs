﻿using AwesomeCare.DataTransferObject.DTOs.Health.Balance;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Balance
{
    public interface IBalanceService
    {
        [Get("/Balance")]
        Task<List<GetBalance>> Get();

        [Get("/Balance/Get/{id}")]
        Task<GetBalance> Get(int id);

        [Get("/Balance/GetbyClient/{id}")]
        Task<GetBalance> GetbyClient(int id);

        [Post("/Balance/Post")]
        Task<HttpResponseMessage> Post([Body] PostBalance model);

        [Put("/Balance/Put")]
        Task<HttpResponseMessage> Put([Body] PostBalance model);

        [Delete("/Balance/Delete/{id}")]
        Task<HttpResponseMessage> Delete(int id);
    }
}
