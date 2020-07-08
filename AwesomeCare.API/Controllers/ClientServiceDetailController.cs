using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientServiceDetailController : ControllerBase
    {
        private readonly IGenericRepository<ClientServiceDetail> clientServiceDetailRepository;
        private readonly IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository;
        private readonly IGenericRepository<Client> clientRepository;
        private readonly ILogger<ClientServiceDetailController> logger;

        public ClientServiceDetailController(IGenericRepository<Model.Models.ClientServiceDetail> clientServiceDetailRepository,
            IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository,
            IGenericRepository<Client> clientRepository,
            ILogger<ClientServiceDetailController> logger)
        {
            this.clientServiceDetailRepository = clientServiceDetailRepository;
            this.staffPersonalInfoRepository = staffPersonalInfoRepository;
            this.clientRepository = clientRepository;
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] PostClientServiceDetail model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var clientServiceDetail = Mapper.Map<ClientServiceDetail>(model);
            var entity = await this.clientServiceDetailRepository.InsertEntity(clientServiceDetail);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetClientServiceDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var clientServices = await (from clserv in clientServiceDetailRepository.Table
                                        join client in clientRepository.Table on clserv.ClientId equals client.ClientId
                                        join staff in staffPersonalInfoRepository.Table on clserv.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                        select new GetClientServiceDetail
                                        {
                                            StaffPersonalInfoId = clserv.StaffPersonalInfoId,
                                            AmountGiven = clserv.AmountGiven,
                                            AmountReturned = clserv.AmountReturned,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ClientId=clserv.ClientId,
                                            ClientServiceDetailId= clserv.ClientServiceDetailId,
                                            ClientServiceDetailItems = (from item in clserv.ClientServiceDetailItems
                                                                        select new GetClientServiceDetailItem
                                                                        {
                                                                            Amount = item.Amount,
                                                                            ClientServiceDetailId = item.ClientServiceDetailId,
                                                                            ClientServiceDetailItemId = item.ClientServiceDetailItemId,
                                                                            ItemName = item.ItemName,
                                                                            Quantity = item.Quantity,
                                                                            Rate = item.Rate
                                                                        }).ToList(),
                                            ClientServiceDetailReceipts = (from receipt in clserv.ClientServiceDetailReceipts
                                                                           select new GetClientServiceDetailReceipt
                                                                           {
                                                                               ClientServiceDetailId = receipt.ClientServiceDetailId,
                                                                               Attachment = receipt.Attachment,
                                                                               ClientServiceDetailReceiptId = receipt.ClientServiceDetailReceiptId
                                                                           }).ToList(),
                                            ServiceDate = clserv.ServiceDate,
                                            Staff = staff.FirstName + " " + staff.MiddleName + " " + staff.LastName
                                        }).ToListAsync();
            return Ok(clientServices);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetClientServiceDetail), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var clientService = await (from clserv in clientServiceDetailRepository.Table
                                        join client in clientRepository.Table on clserv.ClientId equals client.ClientId
                                        join staff in staffPersonalInfoRepository.Table on clserv.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                        where clserv.ClientServiceDetailId == id
                                        select new GetClientServiceDetail
                                        {
                                            StaffPersonalInfoId = clserv.StaffPersonalInfoId,
                                            AmountGiven = clserv.AmountGiven,
                                            AmountReturned = clserv.AmountReturned,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ClientId = clserv.ClientId,
                                            ClientServiceDetailId = clserv.ClientServiceDetailId,
                                            ClientServiceDetailItems = (from item in clserv.ClientServiceDetailItems
                                                                        select new GetClientServiceDetailItem
                                                                        {
                                                                            Amount = item.Amount,
                                                                            ClientServiceDetailId = item.ClientServiceDetailId,
                                                                            ClientServiceDetailItemId = item.ClientServiceDetailItemId,
                                                                            ItemName = item.ItemName,
                                                                            Quantity = item.Quantity,
                                                                            Rate = item.Rate
                                                                        }).ToList(),
                                            ClientServiceDetailReceipts = (from receipt in clserv.ClientServiceDetailReceipts
                                                                           select new GetClientServiceDetailReceipt
                                                                           {
                                                                               ClientServiceDetailId = receipt.ClientServiceDetailId,
                                                                               Attachment = receipt.Attachment,
                                                                               ClientServiceDetailReceiptId = receipt.ClientServiceDetailReceiptId
                                                                           }).ToList(),
                                            ServiceDate = clserv.ServiceDate,
                                            Staff = staff.FirstName + " " + staff.MiddleName + " " + staff.LastName
                                        }).FirstOrDefaultAsync();
            return Ok(clientService);
        }


        [HttpGet("UserClientService")]
        [ProducesResponseType(typeof(List<GetClientServiceDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserClientService()
        {
            var identityUserId = this.User.SubClaim();
            var clientServices = await (from clserv in clientServiceDetailRepository.Table
                                        join client in clientRepository.Table on clserv.ClientId equals client.ClientId
                                        join staff in staffPersonalInfoRepository.Table on clserv.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                        where staff.ApplicationUserId == identityUserId
                                        select new GetClientServiceDetail
                                        {
                                            StaffPersonalInfoId = clserv.StaffPersonalInfoId,
                                            AmountGiven = clserv.AmountGiven,
                                            AmountReturned = clserv.AmountReturned,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ClientId = clserv.ClientId,
                                            ClientServiceDetailId = clserv.ClientServiceDetailId,
                                            ClientServiceDetailItems = (from item in clserv.ClientServiceDetailItems
                                                                        select new GetClientServiceDetailItem
                                                                        {
                                                                            Amount = item.Amount,
                                                                            ClientServiceDetailId = item.ClientServiceDetailId,
                                                                            ClientServiceDetailItemId = item.ClientServiceDetailItemId,
                                                                            ItemName = item.ItemName,
                                                                            Quantity = item.Quantity,
                                                                            Rate = item.Rate
                                                                        }).ToList(),
                                            ClientServiceDetailReceipts = (from receipt in clserv.ClientServiceDetailReceipts
                                                                           select new GetClientServiceDetailReceipt
                                                                           {
                                                                               ClientServiceDetailId = receipt.ClientServiceDetailId,
                                                                               Attachment = receipt.Attachment,
                                                                               ClientServiceDetailReceiptId = receipt.ClientServiceDetailReceiptId
                                                                           }).ToList(),
                                            ServiceDate = clserv.ServiceDate,
                                            Staff = staff.FirstName + " " + staff.MiddleName + " " + staff.LastName
                                        }).ToListAsync();
            return Ok(clientServices);
        }

        [HttpGet("UserClientService/{id}")]
        [ProducesResponseType(typeof(GetClientServiceDetail), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserClientService(int id)
        {
            var identityUserId = this.User.SubClaim();
            var clientService = await (from clserv in clientServiceDetailRepository.Table
                                       join client in clientRepository.Table on clserv.ClientId equals client.ClientId
                                       join staff in staffPersonalInfoRepository.Table on clserv.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                       where clserv.ClientServiceDetailId == id && staff.ApplicationUserId == identityUserId
                                       select new GetClientServiceDetail
                                       {
                                           StaffPersonalInfoId = clserv.StaffPersonalInfoId,
                                           AmountGiven = clserv.AmountGiven,
                                           AmountReturned = clserv.AmountReturned,
                                           Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                           ClientId = clserv.ClientId,
                                           ClientServiceDetailId = clserv.ClientServiceDetailId,
                                           ClientServiceDetailItems = (from item in clserv.ClientServiceDetailItems
                                                                       select new GetClientServiceDetailItem
                                                                       {
                                                                           Amount = item.Amount,
                                                                           ClientServiceDetailId = item.ClientServiceDetailId,
                                                                           ClientServiceDetailItemId = item.ClientServiceDetailItemId,
                                                                           ItemName = item.ItemName,
                                                                           Quantity = item.Quantity,
                                                                           Rate = item.Rate
                                                                       }).ToList(),
                                           ClientServiceDetailReceipts = (from receipt in clserv.ClientServiceDetailReceipts
                                                                          select new GetClientServiceDetailReceipt
                                                                          {
                                                                              ClientServiceDetailId = receipt.ClientServiceDetailId,
                                                                              Attachment = receipt.Attachment,
                                                                              ClientServiceDetailReceiptId = receipt.ClientServiceDetailReceiptId
                                                                          }).ToList(),
                                           ServiceDate = clserv.ServiceDate,
                                           Staff = staff.FirstName + " " + staff.MiddleName + " " + staff.LastName
                                       }).FirstOrDefaultAsync();
            return Ok(clientService);
        }


    }
}
