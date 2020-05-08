


using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Microsoft.AspNetCore.Http
{
    public static class RazorPageExtension
    {
       
        public static AwesomeCare.Admin.Models.OperationStatus OperationStatus(this ITempDataDictionary @this)
        {
            var status = @this["OperationStatus"];
            if(status != null)
            {
                var operationStatus = JsonConvert.DeserializeObject<AwesomeCare.Admin.Models.OperationStatus>(status.ToString());
                return operationStatus;
            }
            return null;
        }
    }
}
