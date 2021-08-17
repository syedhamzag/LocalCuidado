using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Health.Balance
{
    public class PutBalance
    {
        public int BalanceId { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Mobility { get; set; }
        public int Status { get; set; }
    }
}
