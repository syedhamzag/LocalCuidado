using AwesomeCare.Admin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Extensions
{
    public static class RefitApiExceptionExtension
    {
        public static string GetException(this Refit.ApiException apiException)
        {
            List<string> messages = new List<string>();
            string exception = apiException.Content;
            var errors = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(exception);
            foreach (var error in errors)
            {
                foreach (var item in error.Value)
                {
                    messages.Add(item);
                }
            }
           
            string msg = string.Join(",", messages);
            return msg;
        }
    }
}
