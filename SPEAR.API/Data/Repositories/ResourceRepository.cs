using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Resource = SPEAR.Data.Entities.Resource;

namespace SPEAR.Data.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ResourceContext _context;

        public ResourceRepository(ResourceContext context)
        {
            _context = context;
        }
        public IQueryable<Resource> Get()
        {
            return _context.Resources.Where(x => !x.IsDeleted);
        }

        public Resource Get(Guid resourceId)
        {
            return _context.Resources.FirstOrDefault(x => x.ResourceId == resourceId);
        }

        public Resource Create(Resource entity)
        {
            if (entity.ResourceId == Guid.Empty)
            {
                entity.ResourceId = Guid.NewGuid();
            }

            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Update(Resource entity)
        {
            _context.Attach(entity);
            _context.SaveChanges();
        }
    }
}
