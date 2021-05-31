using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class ClientMeal
    {
        public int ClientMealId { get; set; }
        public int ClientId { get; set; }
        /// <summary>
        /// AM, TEA, LUNCH e.t.c.
        /// </summary>
        public int ClientMealTypeId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ClientMealType ClientMealType { get; set; }
        public virtual ICollection<ClientMealDays> ClientMealDays { get; set; }
    }
}
