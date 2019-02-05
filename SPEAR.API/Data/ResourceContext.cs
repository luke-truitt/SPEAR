using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Resource = SPEAR.Data.Entities.Resource;

namespace SPEAR.Data
{
    public class ResourceContext : DbContext
    {
        public virtual DbSet<Resource> Resources { get; set; }

        public ResourceContext(DbContextOptions options) : base(options)
        {
        }
    }
}

