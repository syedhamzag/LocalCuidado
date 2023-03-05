using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeCare.DataTransferObject.DTOs.Google;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Refit;

namespace AwesomeCare.Services.Services
{
    public interface IGoogleService
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origins">latitude,longitude</param>
        /// <param name="destinations">latitude,longitude</param>
        /// <param name="units">e.g imperial</param>
        /// <param name="mode">e.g driving (default), walking, bicycling, transit</param>
        /// <param name="key">google secret key</param>
        /// <returns></returns>
        [Get("/maps/api/distancematrix/json?origins={origins}&destinations={destinations}&units={units}&mode={mode}&key={key}")]
        Task<Refit.ApiResponse<DistanceMatrixResponse>> DistanceMatrix(string origins,string destinations,string units,string mode,string key);
    }
}
