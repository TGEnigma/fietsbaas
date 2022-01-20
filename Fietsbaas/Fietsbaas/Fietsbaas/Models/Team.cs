using System.Collections;
using System.Collections.Generic;

namespace Fietsbaas.Models
{
    public class Team : BaseModel
    {
        public int UserId { get; set; }
        public int RaceId { get; set; }

        public virtual User User { get; set; }
        public virtual Race Race { get; set; }
        public virtual ICollection<TeamRacer> Racers { get; set; }
    }
}
