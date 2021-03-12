using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TwilioIVRController : TwilioController
    {
        private readonly ILogger<TwilioIVRController> logger;

        public TwilioIVRController(ILogger<TwilioIVRController> logger)
        {
            this.logger = logger;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("IVR/Message")]
        public TwiMLResult GetMessage()
        {
            var response = new VoiceResponse();
            //var url = Url.Action("action", "controller");
            //var uri = new System.Uri(url);
            var gather = new Gather(action: Url.ActionUri("action", "controller"), numDigits: 1);
            gather.Say("Thank you for contacting Cuidado. Press 1 to clock in, press 2 to clock out");

            response.Append(gather);

            return TwiML(response);
            // return Ok();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("IVR/Response")]
        public TwiMLResult ProcessResponse(VoiceRequest digits)
        {
            var json = JsonConvert.SerializeObject(digits);
            logger.LogInformation(json);
            var response = new VoiceResponse();
            bool result;
            if(digits.Digits == "1")
            {
                result = ClockIn();
                response.Say("Your response was successfully processed. Thank you");

            }
            else if(digits.Digits == "2")
            {
                result = ClockOut();
                response.Say("Your response was successfully processed. Thank you");

            }
            else
            {
                response.Say("You provided an invalid response. Thank you");

            }
            
            response.Hangup();

            return TwiML(response);
        }

        private bool ClockIn()
        {
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return false;
            }
        }

        private bool ClockOut()
        {
            try
            {

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
