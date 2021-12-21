using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.FilesAndRecord
{
    public class PostFilesAndRecord: BaseDTO 
    {
        public int FilesAndRecordId { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string Subject { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Remarks { get; set; }
        public string Attachment { get; set; }

    }
}
