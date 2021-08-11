using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal
{
    public class GetPersonal
    {
        public int PersonalId { get; set; }
        public int PersonalDetailId { get; set; }
        public int Smoking { get; set; }
        public int DNR { get; set; }
    }
}
