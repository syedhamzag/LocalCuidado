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
        public bool HasGoogleForm { get; set; }
        public string AddLink { get; set; }
        public string ViewLink { get; set; }

        public int ExpiryInMonths { get; set; }
        public virtual BaseRecordModel BaseRecord { get; set; }
    }
}
