﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
   public class GetStaffReferee
    {
        public int StaffRefereeId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public string Referee { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PositionofReferee { get; set; }
        public string Attachment { get; set; }
    }
}
