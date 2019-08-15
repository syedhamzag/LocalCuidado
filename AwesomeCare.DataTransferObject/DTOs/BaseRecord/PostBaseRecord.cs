using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BaseRecord
{
   public class PostBaseRecord
    {
        public PostBaseRecord()
        {
            PostBaseRecordItems = new List<PostBaseRecordItem>();
        }
        [Required]
        [MaxLength(50)]
        public string KeyName { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

        public List<PostBaseRecordItem> PostBaseRecordItems { get; set; }
    }
}
