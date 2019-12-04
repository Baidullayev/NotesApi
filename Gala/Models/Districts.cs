using System;
using System.Collections.Generic;

namespace Gala.Models
{
    public partial class Districts
    {
        public Districts()
        {
            Persons2 = new HashSet<Persons2>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Persons2> Persons2 { get; set; }
    }
}
