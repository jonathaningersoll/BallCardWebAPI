using BBCards.Models.SetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Models
{
    // A manufacturer could be someone like Topps or Donrus.
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public IEnumerable<Set> Sets { get; set; }
    }

    public class ManufacturerResponse
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public IEnumerable<SetResponse> Sets { get; set; } = Array.Empty<SetResponse>();
    }


}
