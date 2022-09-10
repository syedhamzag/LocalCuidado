using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.AuditTrail
{
    public class PostAuditTrail
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public DateTime Duration { get; set; }
    }
}
