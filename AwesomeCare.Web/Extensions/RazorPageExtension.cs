

using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json;

namespace Microsoft.AspNetCore.Http
{
    public static class RazorPageExtension
    {

        public static AwesomeCare.Web.Models.OperationStatus OperationStatus(this RazorPage razorPage)
        {
            var status = razorPage.TempData["OperationStatus"];
            if (status != null)
            {
                var operationStatus = JsonConvert.DeserializeObject<AwesomeCare.Web.Models.OperationStatus>(status.ToString());
                return operationStatus;
            }
            return null;
        }
    }
}
