using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Models.CardModels
{
    public class CardListItem
    {
        public int Id { get; set; }

        public string CardIdentifier { get; set; }
        public Player Player { get; set; }
        public Team Team { get; set; }
    }
}
