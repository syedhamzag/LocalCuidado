﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoteringController : ControllerBase
    {
        private ILogger<RoteringController> _logger;
        private IGenericRepository<ClientRota> _clientRotaRepository;
        private IGenericRepository<ClientRotaType> _clientRotaTypeRepository;
        private IGenericRepository<Client> _clientRepository;
        private IGenericRepository<ClientRotaDays> _clientRotaDaysRepository;
        private IGenericRepository<Rota> _rotaRepository;
        private IGenericRepository<RotaDayofWeek> _rotaDayofWeekRepository;
        private IGenericRepository<StaffRota> _staffRotaRepository;
        private IGenericRepository<StaffPersonalInfo> _staffPersonalInfoRepository;

        public RoteringController(ILogger<RoteringController> logger, IGenericRepository<ClientRota> clientRotaRepository,
            IGenericRepository<ClientRotaType> clientRotaTypeRepository, IGenericRepository<Client> clientRepository,
             IGenericRepository<ClientRotaDays> clientRotaDaysRepository, IGenericRepository<Rota> rotaRepository,
              IGenericRepository<RotaDayofWeek> rotaDayofWeekRepository, IGenericRepository<StaffRota> staffRotaRepository,
              IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository)
        {
            _logger = logger;
            _clientRotaRepository = clientRotaRepository;
            _clientRotaTypeRepository = clientRotaTypeRepository;
            _clientRepository = clientRepository;
            _clientRotaDaysRepository = clientRotaDaysRepository;
            _rotaRepository = rotaRepository;
            _rotaDayofWeekRepository = rotaDayofWeekRepository;
            _staffRotaRepository = staffRotaRepository;
            _staffPersonalInfoRepository = staffPersonalInfoRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDate">StartDate (yyyy-MM-dd)</param>
        /// <param name="eDate">EndDate (yyyy-MM-dd)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("RotaAdmin/{sDate}/{eDate}")]
        [ProducesResponseType(typeof(List<RotaAdmin>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Rota(string sDate, string eDate)
        {
            string format = "yyyy-MM-dd";
            bool isStartDateValid = DateTime.TryParseExact(sDate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime startDate);
            bool isEndDateValid = DateTime.TryParseExact(eDate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime endDate);
            if (!isStartDateValid || !isEndDateValid)
            {
                return BadRequest("Invalid Date format, Format is yyyy-MM-dd");
            }
            var rotas = (from cr in _clientRotaRepository.Table
                         join crt in _clientRotaTypeRepository.Table on cr.ClientRotaTypeId equals crt.ClientRotaTypeId
                         join c in _clientRepository.Table on cr.ClientId equals c.ClientId
                         select new RotaAdmin
                         {
                             ClientRotaId = cr.ClientRotaId,
                             ClientId = cr.ClientId,
                             Period = crt.RotaType,
                             ClientName = c.Firstname + " " + c.Middlename + " " + c.Surname,
                             ClientPostCode = c.PostCode,
                             ClientKeySafe = c.KeySafe,
                             RotaDays = (from crd in cr.ClientRotaDays
                                         join r in _rotaRepository.Table on crd.RotaId equals r.RotaId
                                         join rd in _rotaDayofWeekRepository.Table on crd.RotaDayofWeekId equals rd.RotaDayofWeekId
                                         join sr in _staffRotaRepository.Table on crd.RotaId equals sr.RotaId
                                         join st in _staffPersonalInfoRepository.Table on sr.Staff equals st.StaffPersonalInfoId
                                         where sr.RotaDate >= startDate && sr.RotaDate <= endDate
                                         select new RotaDays
                                         {
                                             DayofWeek = rd.DayofWeek,
                                             StartTime = crd.StartTime,
                                             StopTime = crd.StopTime,
                                             Rota = r.RotaName,
                                             Staff = st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                             RotaDate = sr.RotaDate,
                                             Remark = sr.Remark,
                                             ReferenceNumber = sr.ReferenceNumber,
                                             Partners = (from p in sr.StaffRotaPartners
                                                         join stp in _staffPersonalInfoRepository.Table on p.StaffId equals stp.StaffPersonalInfoId
                                                         select new StaffPartner
                                                         {
                                                             Partner = stp.FirstName + " " + stp.MiddleName + " " + stp.LastName,
                                                             Telephone = stp.Telephone
                                                         }).ToList()
                                         }).ToList()

                         }).ToList();



            return Ok(rotas);
        }
    }
}