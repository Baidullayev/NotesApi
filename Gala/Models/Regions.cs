using System;
using System.Collections.Generic;

namespace Gala.Models
{
    public partial class Regions
    {
        public Regions()
        {
            Persons2 = new HashSet<Persons2>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Persons2> Persons2 { get; set; }
    }
}
