using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryAllowance;
using AwesomeCare.DataTransferObject.DTOs.Staff.SalaryDeduction;
using AwesomeCare.DataTransferObject.DTOs.Staff.StaffTax;
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
    public class PayrollController : ControllerBase
    {
        private IGenericRepository<StaffPersonalInfo> _staffInfoRepository;

        public PayrollController(IGenericRepository<StaffPersonalInfo> staffInfoRepository)
        {
            _staffInfoRepository = staffInfoRepository;
        }
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffProfile>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var staffPayroll = await (from st in _staffInfoRepository.Table
                                select new GetStaffProfile
                                {
                                    StaffPersonalInfoId = st.StaffPersonalInfoId,
                                    CanDrive = st.CanDrive,
                                    StartDate = st.StartDate,
                                    FirstName = st.FirstName,
                                    MiddleName = st.MiddleName,
                                    LastName = st.LastName,
                                    Email = st.Email,
                                    WorkTeam = st.WorkTeam,
                                    ProfilePix = st.ProfilePix,
                                    Rate = st.Rate,
                                    Status = st.Status,
                                    IdNumber = st.IdNumber,
                                }).ToListAsync();

            return Ok(staffPayroll);
        }
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffProfile), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            var staffPayroll = await (from st in _staffInfoRepository.Table
                                      where st.StaffPersonalInfoId == id.Value
                                      select new GetStaffProfile
                                      {
                                          AboutMe = st.AboutMe,
                                          StaffPersonalInfoId = st.StaffPersonalInfoId,
                                          Address = st.Address,
                                          CanDrive = st.CanDrive,
                                          CoverLetter = st.CoverLetter,
                                          CV = st.CV,
                                          DateOfBirth = st.DateOfBirth,
                                          DBS = st.DBS,
                                          DBSAttachment = st.DBSAttachment,
                                          DBSExpiryDate = st.DBSExpiryDate,
                                          DBSUpdateNo = st.DBSUpdateNo,
                                          DrivingLicense = st.DrivingLicense,
                                          DrivingLicenseExpiryDate = st.DrivingLicenseExpiryDate,
                                          EmploymentDate = st.EmploymentDate,
                                          HasIdCard = st.HasIdCard.Value ? "Yes" : "No",
                                          HasUniform = st.HasUniform.Value ? "Yes" : "No",
                                          IsTeamLeader = st.IsTeamLeader.Value ? "Yes" : "No",
                                          Email = st.Email,
                                          StartDate = st.StartDate,
                                          EndDate = st.EndDate,
                                          FirstName = st.FirstName,
                                          Gender = st.Gender,
                                          Hobbies = st.Hobbies,
                                          IdNumber = st.IdNumber,
                                          Keyworker = st.Keyworker,
                                          LastName = st.LastName,
                                          MiddleName = st.MiddleName,
                                          NI = st.NI,
                                          NIAttachment = st.NIAttachment,
                                          NIExpiryDate = st.NIExpiryDate,
                                          Passcode = st.Passcode,
                                          PostCode = st.PostCode,
                                          ProfilePix = st.ProfilePix,
                                          Rate = st.Rate,
                                          PlaceOfBirth = st.PlaceOfBirth,
                                          JobCategory = st.JobCategory,
                                          RegistrationId = st.RegistrationId,
                                          RightToWork = st.RightToWork,
                                          RightToWorkAttachment = st.RightToWorkAttachment,
                                          RightToWorkExpiryDate = st.RightToWorkExpiryDate,
                                          Self_PYE = st.Self_PYE,
                                          Self_PYEAttachment = st.Self_PYEAttachment,
                                          Status = st.Status,
                                          TeamLeader = st.TeamLeader,
                                          Telephone = st.Telephone,
                                          GetSalaryAllowance = (from al in st.SalaryAllowance
                                                                select new GetSalaryAllowance
                                                                {
                                                                    StartDate = al.StartDate,
                                                                    AllowanceType = al.AllowanceType,
                                                                    Amount = al.Amount,
                                                                    EndDate = al.EndDate,
                                                                    Percentage = al.Percentage,
                                                                    Reoccurent = al.Reoccurent,
                                                                    SalaryAllowanceId = al.SalaryAllowanceId,


                                                                }).ToList(),
                                          GetSalaryDeduction = (from de in st.SalaryDeduction
                                                                select new GetSalaryDeduction
                                                                {
                                                                    StartDate = de.StartDate,
                                                                    AllowanceType = de.AllowanceType,
                                                                    Amount = de.Amount,
                                                                    EndDate = de.EndDate,
                                                                    Percentage = de.Percentage,
                                                                    Reoccurent = de.Reoccurent,
                                                                    SalaryDeductionId = de.SalaryDeductionId,
                                                                }).ToList(),
                                          GetStaffTax = (from tax in st.StaffTax
                                                         select new GetStaffTax
                                                         {
                                                             NI = tax.NI,
                                                             Remarks = tax.Remarks,
                                                             Tax = tax.Tax,
                                                             StaffTaxId = tax.StaffTaxId

                                                         }).ToList()


                                      }).FirstOrDefaultAsync();

            return Ok(staffPayroll);
        }
    }
}
