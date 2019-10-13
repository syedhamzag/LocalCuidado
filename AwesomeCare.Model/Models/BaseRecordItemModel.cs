using AwesomeCare.Model.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class BaseRecordItemModel: BaseModel
    {
        public int BaseRecordItemId { get; set; }
        public int BaseRecordId { get; set; }
        public string ValueName { get; set; }
        public virtual BaseRecordModel BaseRecord { get; set; }
    }
}
