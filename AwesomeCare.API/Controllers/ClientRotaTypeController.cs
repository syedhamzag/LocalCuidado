using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    /// <summary>
    /// Client Rota Types such as AM, LUNCH, TEA, BED, OTHERS etc
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRotaTypeController : ControllerBase
    {
        public ClientRotaTypeController()
        {

        }
    }
}