using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CareReview;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CareReviewController : ControllerBase
    {
        private IGenericRepository<CareReview> _carereviewRepository;

        public CareReviewController(IGenericRepository<CareReview> CareReviewRepository)
        {

            _carereviewRepository = CareReviewRepository;
        }
        #region CareReview
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetCareReview>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _carereviewRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostCareReview post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CareReview = Mapper.Map <CareReview>(post);
            await _carereviewRepository.InsertEntity(CareReview);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutCareReview models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CareReview = Mapper.Map<CareReview>(models);
            await _carereviewRepository.UpdateEntity(CareReview);
            return Ok();

        }

        [HttpGet("GetByClient/{id}")]
        [ProducesResponseType(type: typeof(GetCareReview), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _carereviewRepository.Table
                                   where c.ClientId == id.Value
                                   select new GetCareReview
                                   {
                                       CareReviewId = c.CareReviewId,
                                       Name = c.Name,
                                       Note = c.Note,
                                       Date = c.Date,
                                       ClientId = c.ClientId,
                                       Action = c.Action,
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetCareReview), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _carereviewRepository.Table
                                   where c.CareReviewId == id.Value
                                   select new GetCareReview
                                   {
                                       CareReviewId = c.CareReviewId,
                                       Name = c.Name,
                                       Note = c.Note,
                                       Date = c.Date,
                                       ClientId = c.ClientId,
                                       Action = c.Action,
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
        #endregion
    }
}
