using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.Enums;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace AwesomeCare.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TwilioIVRController : TwilioController
    {
        private readonly ILogger<TwilioIVRController> logger;
        private readonly IGenericRepository<StaffRotaPeriod> staffRotaPeriodRepository;
        private readonly IGenericRepository<Model.Models.Client> clientRepository;

        public TwilioIVRController(ILogger<TwilioIVRController> logger,
            IGenericRepository<StaffRotaPeriod> staffRotaPeriodRepository,
            IGenericRepository<Model.Models.Client> clientRepository)
        {
            this.logger = logger;
            this.staffRotaPeriodRepository = staffRotaPeriodRepository;
            this.clientRepository = clientRepository;
        }

        [HttpPost("IVR/Message")]
        public TwiMLResult GetMessage()
        {
            var response = new VoiceResponse();
            var url = Url.Action("ProcessResponse", "TwilioIVR");
            var fullUrl = $"{this.HttpContext.Request.Scheme}://{this.HttpContext.Request.Host.Host}{url}";
            var uri = new System.Uri(fullUrl);
            var gather = new Gather(action: uri, numDigits: 1);
            gather.Say("Thank you for contacting Cuidado. Press 1 to clock in, press 2 to clock out");

            response.Append(gather);

            return TwiML(response);
            // return Ok();
        }

        [HttpPost("IVR/Response")]
        public TwiMLResult ProcessResponse(string response)
        {
            var voiceResponse = new VoiceResponse();
            var forms = this.HttpContext.Request.Form;

            var digits = forms["Digits"].FirstOrDefault();
            var caller = forms["From"].FirstOrDefault();

            bool isPhoneValid = clientRepository.Table.Any(c => c.Telephone == caller);
            if (!isPhoneValid)
            {
                voiceResponse.Say("Invalid Caller Id");
                return TwiML(voiceResponse);
            }

            bool isValid = true;
            System.Uri uri = null;

            if (digits == "1")
            {
                var url = Url.Action("ProcessClockIn", "TwilioIVR");
                var fullUrl = $"{this.HttpContext.Request.Scheme}://{this.HttpContext.Request.Host.Host}{url}";
                uri = new System.Uri(fullUrl);

            }
            else if (digits == "2")
            {
                var url = Url.Action("ProcessClockOut", "TwilioIVR");
                var fullUrl = $"{this.HttpContext.Request.Scheme}://{this.HttpContext.Request.Host.Host}{url}";
                uri = new System.Uri(fullUrl);

            }
            else
            {
                isValid = false;
            }

            var gather = new Gather(action: uri, finishOnKey: "#");

            if (isValid)
                gather.Say("Thank you for contacting Cuidado. Kindly enter the Rota Id on your mobile App and press the pound key.");
            // gather.Say("Thank you for contacting Cuidado. For AM Press 1, For LUNCH Press 2, For TEA Press 3, For BED Press 4, For OTHERS 1 press 5, For OTHERS 2 press 6, For OTHERS 3 press 7, For OTHERS 4 press 8");
            else
                gather.Say("You provided an invalid response. Thank you");

            voiceResponse.Append(gather);

            return TwiML(voiceResponse);

        }

        [HttpPost("IVR/ClockIn")]
        public async Task<TwiMLResult> ProcessClockIn(string response)
        {
            var voiceResponse = new VoiceResponse();
            var forms = this.HttpContext.Request.Form;

            var digits = forms["Digits"].FirstOrDefault();
            var caller = forms["From"].FirstOrDefault();

            bool result;
            result = await ClockIn(digits);

            if (result)
                voiceResponse.Say("Thank you for contacting Cuidado, your request was successfully processed");
            else
                voiceResponse.Say("You provided an invalid response");

            return TwiML(voiceResponse);
        }

        [HttpPost("IVR/ClockOut")]
        public async Task<TwiMLResult> ProcessClockOut(string response)
        {
            var voiceResponse = new VoiceResponse();
            var forms = this.HttpContext.Request.Form;

            var digits = forms["Digits"].FirstOrDefault();
            var caller = forms["From"].FirstOrDefault();

            bool result;
            result = await ClockOut(digits);

            if (result)
                voiceResponse.Say("Thank you for contacting Cuidado, your request was successfully processed");
            else
                voiceResponse.Say("You provided an invalid response");

            return TwiML(voiceResponse);
        }
        private async Task<bool> ClockIn(string rotaId)
        {
            try
            {

                int staffRotaId = int.TryParse(rotaId, out int rtId) ? rtId : 0;
                var rota = await staffRotaPeriodRepository.Table.FirstOrDefaultAsync(r => r.StaffRotaPeriodId == staffRotaId);
                if (rota == null)
                    return false;

               
                rota.ClockInTime = DateTimeOffset.UtcNow;
                rota.ClockInMode = ClockModeEnum.Twilio.ToString();

                var id = await staffRotaPeriodRepository.UpdateEntity(rota);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return false;
            }
        }

        private async Task<bool> ClockOut(string rotaId)
        {
            try
            {

                int staffRotaId = int.TryParse(rotaId, out int rtId) ? rtId : 0;
                var rota = await staffRotaPeriodRepository.Table.FirstOrDefaultAsync(r => r.StaffRotaPeriodId == staffRotaId);
                if (rota == null)
                    return false;

               
                rota.ClockOutTime = DateTimeOffset.UtcNow;
                rota.ClockOutMode = ClockModeEnum.Twilio.ToString();

                var id = await staffRotaPeriodRepository.UpdateEntity(rota);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return false;
            }
        }




    }
}
