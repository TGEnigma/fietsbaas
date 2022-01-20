using System;
using System.Collections.Generic;
using System.Text;

namespace Fietsbaas.Models
{
    public class Race : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Stage> Stages { get; set; }
        public virtual ICollection<Racer> Racers { get; set; }
    }
}
