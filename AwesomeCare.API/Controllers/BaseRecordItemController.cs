using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BaseRecordItem;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseRecordItemController : ControllerBase
    {

        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;
        private ILogger<BaseRecordItemController> _logger;

        public BaseRecordItemController(IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, ILogger<BaseRecordItemController> logger)
        {
            _baseRecordItemRepository = baseRecordItemRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetBaseRecordItem), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBaseRecordItem([FromBody]PutBaseRecordItem item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var baseRecordItem = Mapper.Map<BaseRecordItemModel>(item);
            var update = await _baseRecordItemRepository.UpdateEntity(baseRecordItem);

            GetBaseRecordItem getBaseRecordItems = Mapper.Map<GetBaseRecordItem>(update);
            return Ok(getBaseRecordItems);
        }
    }
}