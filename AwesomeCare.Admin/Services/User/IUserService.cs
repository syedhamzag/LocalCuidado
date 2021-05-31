using AwesomeCare.DataTransferObject.DTOs.User;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.User
{
    public interface IUserService
    {
        [Get("/User")]
        Task<List<GetUser>> GetUsers();

        [Get("/User/{userId}")]
        Task<GetUser> GetUser(string userId);

        [Put("/User")]
        Task<HttpResponseMessage> UpdateUser([Body]PutUser model);
    }
}
