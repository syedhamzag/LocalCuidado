using AwesomeCare.DataTransferObject.DTOs.Dashboard;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Dashboard
{
    public interface IDashboardService
    {
        [Get("/Dashboard")]
        Task<GetDashboard> Get();
    }
}
