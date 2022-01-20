namespace Fietsbaas.Models
{
    public class RacerStageProgress : BaseModel
    {
        public Racer Participant { get; set; }
        public Stage Stage { get; set; }
        public RacerStatus Status { get; set; }
        public int? Position { get; set; }
    }
}
