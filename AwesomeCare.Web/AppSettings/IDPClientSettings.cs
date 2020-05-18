using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.AppSettings
{
    public class IDPClientSettings
    {
        public IDPClientSettings()
        {
            Scopes = new List<string>();
        }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public List<string> Scopes { get; set; }
    }
}
