﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.BaseRecordItem
{
   public class PutBaseRecordItem
    {
        [Required]
        public int BaseRecordItemId { get; set; }
        [Required]
        public int BaseRecordId { get; set; }
        [Required]
        public string ValueName { get; set; }
        [Required]
        public bool Deleted { get; set; }
        public bool HasGoogleForm { get; set; }
        public string AddLink { get; set; }
        public string ViewLink { get; set; }
        public int ExpiryInMonths { get; set; }
    }
}
