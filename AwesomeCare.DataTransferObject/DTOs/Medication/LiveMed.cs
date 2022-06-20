using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Medication
{
    public class LiveMed
    {
        
        public List<GroupLiveMed> groupLiveMeds { get; set; } = new List<GroupLiveMed>();

    }
}
