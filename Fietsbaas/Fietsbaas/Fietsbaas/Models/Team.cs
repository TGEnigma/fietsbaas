using System.Collections.Generic;

namespace Fietsbaas.Models
{
    public class Team : BaseModel
    {
        public Race Race { get; set; }
        public List<Racer> Racers { get; set; }
        public List<Racer> ReserveRacers { get; set; }
        public List<Bet> Bets { get; }
    }
}
