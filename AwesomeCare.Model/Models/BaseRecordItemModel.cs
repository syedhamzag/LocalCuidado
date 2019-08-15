using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class BaseRecordItemModel
    {
        public int BaseRecordItemId { get; set; }
        public int BaseRecordId { get; set; }
        public string ValueName { get; set; }
        public bool Deleted { get; set; }
        public virtual BaseRecordModel BaseRecord { get; set; }
    }
}
