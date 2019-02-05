using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPEAR.Data.Entities;

namespace SPEAR.Data.Repositories
{
    public interface IResourceRepository
    {
        IQueryable<Resource> Get();
        Resource Get(Guid resourceId);
        void Update(Resource entity);
        Resource Create(Resource entity);
    }
}
