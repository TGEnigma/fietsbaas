using System;
using System.Text;

namespace Fietsbaas.Models
{
    public class Race : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
