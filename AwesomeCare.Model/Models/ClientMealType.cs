using System;
using System.Collections.Generic;
using System.Text;
using AwesomeCare.Model.Models.Base;
namespace AwesomeCare.Model.Models
{
  public  class ClientMealType:BaseModel
    {
        public ClientMealType()
        {
            ClientMeal = new HashSet<ClientMealDays>();
        }
        public int ClientMealTypeId { get; set; }
        public string MealType { get; set; }

        public virtual ICollection<ClientMealDays> ClientMeal { get; set; }
    }
}
