namespace Fietsbaas.Models
{
    public class TeamRacer : BaseModel
    {
        public int TeamId { get; set; }
        public int RacerId { get; set; }
        public bool IsReserve { get; set; }
        public BetType Bet { get; set; }

        public virtual Team Team { get; set; }
        public virtual Racer Racer { get; set; }
    }
}
