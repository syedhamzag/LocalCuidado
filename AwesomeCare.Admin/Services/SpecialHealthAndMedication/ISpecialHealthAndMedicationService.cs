﻿using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthAndMedication;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.SpecialHealthAndMedication
{
    public interface ISpecialHealthAndMedicationService
    {
        [Get("/SpecialHealthAndMedication")]
        Task<List<GetSpecialHealthAndMedication>> Get();

        [Get("/SpecialHealthAndMedication/Get/{id}")]
        Task<GetSpecialHealthAndMedication> Get(int id);

        [Get("/SpecialHealthAndMedication/GetbyClient/{id}")]
        Task<GetSpecialHealthAndMedication> GetbyClient(int id);

        [Post("/SpecialHealthAndMedication/Post")]
        Task<HttpResponseMessage> Post([Body] PostSpecialHealthAndMedication model);

        [Put("/SpecialHealthAndMedication/Put")]
        Task<HttpResponseMessage> Put([Body] PostSpecialHealthAndMedication model);

        [Delete("/SpecialHealthAndMedication/Delete/{id}")]
        Task<HttpResponseMessage> Delete(int id);

    }
}
