using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Models
{
    // A set could be Topps flagship series or Chrome series, or Finest series.
    public class Set
    {
        public int SetId { get; set; }
        public string SetName { get; set; }
        public int SetYear { get; set; }
        public IEnumerable<Card> Cards{ get; set; }

        public int ManufacturerId { get; set; }
    }
}
