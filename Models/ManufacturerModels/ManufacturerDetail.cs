using BBCards.Models.SetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Models.ManufacturerModels
{
    public class ManufacturerDetail
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public IEnumerable<SetListItem> Sets { get; set; }
    }
}
