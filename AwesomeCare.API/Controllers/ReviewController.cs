using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<Review> _ReviewRepository;

        public ReviewController(AwesomeCareDbContext dbContext, IGenericRepository<Review> ReviewRepository)
        {
            _ReviewRepository = ReviewRepository;
            _dbContext = dbContext;
        }
        #region Review
        /// <summary>
        /// Get All Review
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetReview>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ReviewRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create Review
        /// </summary>
        /// <param name="postReview"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostReview postReview)
        {
            if (postReview == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Review = Mapper.Map<Review>(postReview);
            await _ReviewRepository.InsertEntity(Review);
            return Ok();
        }
        /// <summary>
        /// Update Review
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostReview models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Review = Mapper.Map<Review>(models);
            await _ReviewRepository.UpdateEntity(Review);
            return Ok();

        }
        /// <summary>
        /// Get Review by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetReview), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getReview = await (from c in _ReviewRepository.Table
                                   where c.ClientId == id.Value
                                   select new GetReview
                                           {
                                               ReviewId = c.ReviewId,
                                               ClientId = c.ClientId,
                                               CP_PreDate = c.CP_PreDate,
                                               CP_ReviewDate = c.CP_ReviewDate,
                                               RA_PreDate = c.RA_PreDate,
                                               RA_ReviewDate = c.RA_ReviewDate
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getReview);
        }
        #endregion
    }
}