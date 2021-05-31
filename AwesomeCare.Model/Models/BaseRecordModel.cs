using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
  public class BaseRecordModel
    {
        public BaseRecordModel()
        {
            BaseRecordItems = new HashSet<BaseRecordItemModel>();
        }
        public int BaseRecordId { get; set; }
        public string KeyName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BaseRecordItemModel> BaseRecordItems { get; set; }
    }
}
