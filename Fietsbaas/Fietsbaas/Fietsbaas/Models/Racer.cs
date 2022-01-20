namespace Fietsbaas.Models
{
    public class Racer : BaseModel
    {
        public Race Race { get; set; }
        public Cyclist Cyclist { get; set; }
        public RacerStatus Status { get; set; }
        public int? Position { get; set; }
    }
}
