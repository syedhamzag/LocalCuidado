using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Investigation;
using AwesomeCare.DataTransferObject.DTOs.InvestigationAttachment;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvestigationController : ControllerBase
    {
        private readonly IGenericRepository<Investigation> investigationRepository;
        private readonly IGenericRepository<InvestigationAttachment> investigationAttachmentRepository;
        private readonly IGenericRepository<Client> clientRepository;
        private readonly IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository;
        private readonly IGenericRepository<BaseRecordItemModel> baseRecordItemRepository;

        public InvestigationController(IGenericRepository<Investigation> investigationRepository,
            IGenericRepository<InvestigationAttachment> investigationAttachmentRepository,
            IGenericRepository<Client> clientRepository,
            IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository,
            IGenericRepository<BaseRecordItemModel> baseRecordItemRepository)
        {
            this.investigationRepository = investigationRepository;
            this.investigationAttachmentRepository = investigationAttachmentRepository;
            this.clientRepository = clientRepository;
            this.staffPersonalInfoRepository = staffPersonalInfoRepository;
            this.baseRecordItemRepository = baseRecordItemRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] PostInvestigation model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var investigation = Mapper.Map<Investigation>(model);
            await investigationRepository.InsertEntity(investigation);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetInvestigation>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var investigations = await (from inv in investigationRepository.Table
                                        join client in clientRepository.Table on inv.ClientId equals client.ClientId
                                        join staff in staffPersonalInfoRepository.Table on inv.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                        join baseRecord in baseRecordItemRepository.Table on inv.IncidentClass equals baseRecord.BaseRecordItemId
                                        select new GetInvestigation
                                        {
                                            ClientId = inv.ClientId,
                                            StaffPersonalInfoId = inv.StaffPersonalInfoId,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ConclusionDate = inv.ConclusionDate,
                                            IncidentClass = baseRecord.ValueName,
                                            IncidentClassId = inv.IncidentClass,
                                            IncidentDate = inv.IncidentDate,
                                            InvestigationId = inv.InvestigationId,
                                            Remark = inv.Remark,
                                            Staff = staff.FirstName + " " + staff.MiddleName + " " + staff.LastName,
                                            InvestigationAttachments = (from att in inv.InvestigationAttachments
                                                                        select new GetInvestigationAttachment
                                                                        {
                                                                            Attachment = att.Attachment,
                                                                            InvestigationAttachmentId = att.InvestigationAttachmentId,
                                                                            InvestigationId = att.InvestigationId
                                                                        }).ToList()
                                        }).ToListAsync();

            return Ok(investigations);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<GetInvestigation>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var investigation = await (from inv in investigationRepository.Table
                                        join client in clientRepository.Table on inv.ClientId equals client.ClientId
                                        join staff in staffPersonalInfoRepository.Table on inv.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                        join baseRecord in baseRecordItemRepository.Table on inv.IncidentClass equals baseRecord.BaseRecordItemId
                                        where inv.InvestigationId == id
                                        select new GetInvestigation
                                        {
                                            ClientId = inv.ClientId,
                                            StaffPersonalInfoId = inv.StaffPersonalInfoId,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ConclusionDate = inv.ConclusionDate,
                                            IncidentClass = baseRecord.ValueName,
                                            IncidentClassId = inv.IncidentClass,
                                            IncidentDate = inv.IncidentDate,
                                            InvestigationId = inv.InvestigationId,
                                            Remark = inv.Remark,
                                            Staff = staff.FirstName + " " + staff.MiddleName + " " + staff.LastName,
                                            InvestigationAttachments = (from att in inv.InvestigationAttachments
                                                                        select new GetInvestigationAttachment
                                                                        {
                                                                            Attachment = att.Attachment,
                                                                            InvestigationAttachmentId = att.InvestigationAttachmentId,
                                                                            InvestigationId = att.InvestigationId
                                                                        }).ToList()
                                        }).FirstOrDefaultAsync();

            return Ok(investigation);
        }
    }
}
