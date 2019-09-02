using AwesomeCare.DataTransferObject.DTOs.BaseRecordItem;
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
            BaseRecordItems = new List<BaseRecordItemDto>();
        }
        [Required]
        [MaxLength(50)]
        public string KeyName { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

        public List<BaseRecordItemDto> BaseRecordItems { get; set; }
    }
}
