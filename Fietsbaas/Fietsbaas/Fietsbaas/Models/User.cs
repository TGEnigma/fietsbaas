﻿using System.Collections.Generic;

namespace Fietsbaas.Models
{
    public class User : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
