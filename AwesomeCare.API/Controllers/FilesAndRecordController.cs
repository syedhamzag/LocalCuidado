using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.FilesAndRecord;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilesAndRecordController : ControllerBase
    {
        private IGenericRepository<FilesAndRecord> _FilesAndRecordRepository;

        public FilesAndRecordController(IGenericRepository<FilesAndRecord> FilesAndRecordRepository)
        {

            _FilesAndRecordRepository = FilesAndRecordRepository;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetFilesAndRecord>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _FilesAndRecordRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostFilesAndRecord post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var FilesAndRecord = Mapper.Map<FilesAndRecord>(post);
            await _FilesAndRecordRepository.InsertEntity(FilesAndRecord);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutFilesAndRecord models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var FilesAndRecord = Mapper.Map<FilesAndRecord>(models);
            await _FilesAndRecordRepository.UpdateEntity(FilesAndRecord);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetFilesAndRecord), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _FilesAndRecordRepository.Table
                                   where c.FilesAndRecordId == id.Value
                                   select new GetFilesAndRecord
                                   {
                                       FilesAndRecordId = c.FilesAndRecordId,
                                       Date = c.Date,
                                       Subject = c.Subject,
                                       Remarks = c.Remarks,
                                       ClientId = c.ClientId,
                                       Attachment = c.Attachment,
                                       StaffPersonalInfoId = c.StaffPersonalInfoId
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
    }
}
