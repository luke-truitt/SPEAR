using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using SPEAR.Data.Enums;
using Type = SPEAR.Data.Enums.Type;

namespace SPEAR.Data.Entities
{
    public class Resource
    {
        public Guid ResourceId { get; set; }
        public bool Available { get; set; }
        public Location Location { get; set; }
        public Type ResourceType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
