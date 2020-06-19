using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Communication;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        private IGenericRepository<Communication> _communicationRepository;
        private IGenericRepository<StaffPersonalInfo> _staffPersonalInfoRepository;
        private ILogger<CommunicationController> _logger;

        public CommunicationController(IGenericRepository<Communication> communicationRepository, ILogger<CommunicationController> logger,
            IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository)
        {
            _communicationRepository = communicationRepository;
            _logger = logger;
            _staffPersonalInfoRepository = staffPersonalInfoRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]PostCommunication model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            // var identities = this.User.Claims.ToList() ;
            var communication = Mapper.Map<Communication>(model);
            var userId = this.User.SubClaim();
            communication.From = userId;
            var entity = await _communicationRepository.InsertEntity(communication);

            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCommunication), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var userId = this.User.SubClaim();
            


            var inboxMessages = await (from ms in _communicationRepository.Table
                                  join sender in _staffPersonalInfoRepository.Table on ms.From equals sender.ApplicationUserId
                                  where ms.To == userId 
                                  select new InboxMessage
                                  {                                     
                                      From = ms.From,
                                      Sender = sender.FirstName + " " + sender.MiddleName + " " + sender.LastName,
                                      CommuncationDate = ms.CommuncationDate,
                                      CommunicationId = ms.CommunicationId,
                                      Message = ms.Message,
                                      IsRead = ms.IsRead,
                                      Subject = ms.Subject
                                  }).OrderBy(d=>d.CommuncationDate).ToListAsync();

            var sentMessages = await (from ms in _communicationRepository.Table
                                       join receiver in _staffPersonalInfoRepository.Table on ms.To equals receiver.ApplicationUserId
                                       where ms.From == userId
                                       select new SentMessage
                                       {
                                           To = ms.To,
                                           From = ms.From,
                                           Receiver = receiver.FirstName + " " + receiver.MiddleName + " " + receiver.LastName,
                                           CommuncationDate = ms.CommuncationDate,
                                           CommunicationId = ms.CommunicationId,
                                           Message = ms.Message,
                                           IsRead = ms.IsRead,
                                           Subject = ms.Subject
                                       }).OrderBy(d => d.CommuncationDate).ToListAsync();
            var messages = new GetCommunication 
            {
                InboxMessages = inboxMessages,
                SentMessages = sentMessages,
                TotalReceived =inboxMessages.Count,
                TotalSent = sentMessages.Count
            };


            return Ok(messages);
        }
      
        [HttpGet("Inbox/{messageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(InboxMessage), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInboxMessage(int messageId)
        {
            var userId = this.User.SubClaim();

            //mark message as read
            var msg =  _communicationRepository.Table.FirstOrDefault(m=>m.CommunicationId == messageId && m.To == userId);
            if (msg == null)
                return NotFound();

            msg.IsRead = true;
            await _communicationRepository.UpdateEntity(msg);

            var inboxMessages = await (from ms in _communicationRepository.Table
                                       join sender in _staffPersonalInfoRepository.Table on ms.From equals sender.ApplicationUserId
                                       where ms.To == userId && ms.CommunicationId == messageId
                                       select new InboxMessage
                                       {
                                           From = ms.From,
                                           Sender = sender.FirstName + " " + sender.MiddleName + " " + sender.LastName,
                                           CommuncationDate = ms.CommuncationDate,
                                           CommunicationId = ms.CommunicationId,
                                           Message = ms.Message,
                                           IsRead = ms.IsRead,
                                           Subject = ms.Subject
                                       }).FirstOrDefaultAsync();

            return Ok(inboxMessages);
        }

        [HttpGet("Sent/{messageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SentMessage), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSentMessage(int messageId)
        {
            var userId = this.User.SubClaim();
           

            var sentMessages = await (from ms in _communicationRepository.Table
                                      join receiver in _staffPersonalInfoRepository.Table on ms.To equals receiver.ApplicationUserId
                                      where ms.From == userId && ms.CommunicationId == messageId
                                      select new SentMessage
                                      {
                                          To = ms.To,
                                          From = ms.From,
                                          Receiver = receiver.FirstName + " " + receiver.MiddleName + " " + receiver.LastName,
                                          CommuncationDate = ms.CommuncationDate,
                                          CommunicationId = ms.CommunicationId,
                                          Message = ms.Message,
                                          IsRead = ms.IsRead,
                                          Subject = ms.Subject
                                      }).FirstOrDefaultAsync();


            return Ok(sentMessages);
        }
    }
}