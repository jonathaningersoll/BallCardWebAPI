using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBCards.Models.CardModels;
using BBCards.Models.ManufacturerModels;

namespace BBCards.Models.SetModels
{
    public class SetDetail
    {
        public int SetId { get; set; }
        public string SetName { get; set; }
        public int SetYear { get; set; }
        public IEnumerable<CardListItem> Cards { get; set; }

        public ManufacturerListItem Manufacturer { get; set; }
    }
}
