using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal
{
    public class PutPersonal
    {
        public int PersonalId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Smoking { get; set; }
        public int DNR { get; set; }
    }
}
