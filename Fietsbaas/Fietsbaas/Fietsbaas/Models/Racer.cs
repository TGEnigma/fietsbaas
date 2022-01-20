namespace Fietsbaas.Models
{
    public class Racer : BaseModel
    {
        public int RaceId { get; set; }
        public int CyclistId { get; set; }
        public RacerStatus Status { get; set; }
        public int? Position { get; set; }

        public virtual Race Race { get; set; }
        public virtual Cyclist Cyclist { get; set; }
    }
}
