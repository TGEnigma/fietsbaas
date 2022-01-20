namespace Fietsbaas.Models
{
    public class Bet : BaseModel
    {
        public BetType Type { get; set; }
        public Racer Racer { get; set; }
    }
}
