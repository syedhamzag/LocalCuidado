using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Chat;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class ChatController : ControllerBase
    {
        private IGenericRepository<Chat> _chatRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;

        public ChatController(IGenericRepository<Chat> chatRepository, IGenericRepository<StaffPersonalInfo> staffRepository)
        {
            _chatRepository = chatRepository;
            _staffRepository = staffRepository;
        }
        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetChat>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var allchats = await _chatRepository.Table.ToListAsync();
            var chats = Mapper.Map<List<GetChat>>(allchats);
            return Ok(chats);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostChat postChat)
        {
            if (postChat == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Chat = Mapper.Map<Chat>(postChat);
            await _chatRepository.InsertEntity(Chat);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutChat> putChat)
        {
            if (putChat == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Chat = Mapper.Map<Chat>(putChat);
            await _chatRepository.UpdateEntity(Chat);

            return Ok();
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetChat), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getChat = await (from c in _chatRepository.Table
                                           where c.ChatId == id.Value
                                           select new GetChat
                                           {
                                               ChatId = c.ChatId,
                                               Dated = c.Dated,
                                               Message = c.Message,
                                               Type = c.Type,
                                               ReceiverId = c.ReceiverId,
                                               SenderId = c.SenderId,
                                               SenderName = _staffRepository.Table.Where(s=>s.StaffPersonalInfoId==c.SenderId).Select(s=>s.FirstName+" "+s.MiddleName+" "+s.LastName).FirstOrDefault(),
                                               ReceiverName = _staffRepository.Table.Where(s=>s.StaffPersonalInfoId==c.ReceiverId).Select(s=>s.FirstName+" "+s.MiddleName+" "+s.LastName).FirstOrDefault()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getChat);
        }
    }
}
