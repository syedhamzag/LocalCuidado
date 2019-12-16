using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.RotaDayofWeek
{
   public interface IRotaDayofWeekService
    {
        [Get("/RotaDayofWeek")]
        Task<List<GetRotaDayofWeek>> Get();

        [Post("/RotaDayofWeek")]
        Task<GetRotaDayofWeek> Post([Body]PostRotaDayofWeek model);

        [Get("/RotaDayofWeek/{id}")]
        Task<GetRotaDayofWeek> Get(int id);

        [Put("/RotaDayofWeek")]
        Task<GetRotaDayofWeek> Put([Body]PutRotaDayofWeek model);
    }
}
