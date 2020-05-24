using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.StaffBlackList;
using Refit;

namespace AwesomeCare.Admin.Services.StaffBlackLIst
{
    public interface IStaffBlackListService
    {
        [Get("/BlackList/Staff")]
        Task<List<GetStaffBlackList>> Get();

        [Post("/BlackList/Staff")]
        Task<HttpResponseMessage> Post([Body]PostStaffBlackList model);

        [Get("/BlackList/Staff/{id}")]
        Task<GetStaffBlackList> Get(int id);

        [Delete("/BlackList/Staff/{id}")]
        Task<HttpResponseMessage> Delete(int id);
    }
}
