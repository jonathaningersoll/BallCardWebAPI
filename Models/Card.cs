using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCards.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string CardIdentifier { get; set; }
        public string Feature { get; set; }             //enum team card, stadium card, etc...
        public string Position { get; set; }            //enum the positions
        public bool IsMainSet { get; set; }
        public bool IsInsert { get; set; }
        public string ParalellColor { get; set; }       //enum the colors
        public bool IsRookieCard { get; set; }
        public string FlavorText { get; set; }

        public int SetId { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
