using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Client.CareObj
{
    public class PostClientCareObj
    {
        public int CareObjId { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }

        public List<PostCareObjPersonToAct> PersonToAct { get; set; } = new List<PostCareObjPersonToAct>();
    }
}
