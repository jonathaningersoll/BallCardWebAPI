using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Models.TeamModels
{

    // This model is in case I need to add more detail to the
    // Team model and want to prevent it from being initially loaded.
    public class TeamListItem
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
