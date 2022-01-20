namespace Fietsbaas.Models
{
    public class RacerStageProgress : BaseModel
    {
        public int ParticipantId { get; set; }
        public int StageId { get; set; }
        public RacerStatus Status { get; set; }
        public int? Position { get; set; }

        public virtual Racer Participant { get; set; }
        public virtual Stage Stage { get; set; }
    }
}
