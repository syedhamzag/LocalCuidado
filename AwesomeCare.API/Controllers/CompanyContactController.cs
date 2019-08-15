using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/Company/{companyId}/[controller]")]
    [ApiController]
    public class CompanyContactController : ControllerBase
    {

        private IGenericRepository<CompanyContactModel> _companyContactRepository;
        private ILogger<CompanyContactController> _logger;

        public CompanyContactController(IGenericRepository<CompanyContactModel> companyContactRepository, ILogger<CompanyContactController> logger)
        {
            _companyContactRepository = companyContactRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetCompanyContactByCompanyId")]
        public async Task<IActionResult> GetCompanyContactByCompanyId(int? companyId)
        {
            if (!companyId.HasValue)
            {
                return NotFound();
            }

            var companyContact = await _companyContactRepository.Table
               .FirstOrDefaultAsync(cc => cc.CompanyId == companyId);

            if (companyContact == null)
            {
                return NotFound();
            }

            var companyContactDto = Mapper.Map<GetCompanyContactDto>(companyContact);

            return Ok(companyContactDto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyContact(int? companyId, int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var companyContact = await _companyContactRepository.Table
               .FirstOrDefaultAsync(cc => cc.CompanyId == companyId && cc.CompanyContactId == id);

            if (companyContact == null)
            {
                return NotFound();
            }

            var companyContactDto = Mapper.Map<GetCompanyContactDto>(companyContact);

            return Ok(companyContactDto);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetCompanyContacts(int companyId)
        //{

        //    var companyContacts = await _companyContactRepository.Table
        //       .Where(cc => cc.CompanyId == companyId)
        //       .ProjectTo<GetCompanyContactDto>().ToListAsync();

        //    return Ok(companyContacts);
        //}

        [HttpPost]
        public async Task<IActionResult> PostCompanyContact(int companyId, [FromBody]PostCompanyContactDto contactDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (companyId != contactDto.CompanyId)
                {
                    return BadRequest("Invalid CompanyId");
                }
                var companyContactMap = Mapper.Map<CompanyContactModel>(contactDto);
                var companyContact = await _companyContactRepository.InsertEntity(companyContactMap);

                var companyContactDto = Mapper.Map<GetCompanyContactDto>(companyContact);

                return CreatedAtRoute("GetCompanyContact", new { id = companyContact.CompanyContactId }, companyContactDto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "PostCompanyContact");
                return BadRequest();
            }
        }
    }
}