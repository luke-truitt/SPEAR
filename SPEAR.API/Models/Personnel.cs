using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPEAR.Data.Enums;

namespace SPEAR.Models
{
    public class Personnel
    {
        public Guid PersonnelId { get; set; }
        public bool Available { get; set; }
        public Location Location { get; set; }
    }
}
