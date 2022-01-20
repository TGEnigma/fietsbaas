namespace Fietsbaas.Models
{
    public class Stage : BaseModel
    {
        public int RaceId { get; set; }
        public string Name { get; set; }

        public virtual Race Race { get; set; }
    }
}
