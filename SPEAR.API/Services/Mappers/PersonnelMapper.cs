using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPEAR.Data.Entities;
using SPEAR.Models;
using Type = SPEAR.Data.Enums.Type;

namespace SPEAR.Services.Mappers
{
    public static class PersonnelMapper
    {
        public static Resource ToPersonnelEntity(this Personnel model)
        {
            var entity = new Resource
            {
                IsActive = true,
                IsDeleted = false,
                ResourceType = Type.Personnel
            };
            return model.UpdatePersonnelEntity(entity);
        }

        public static Resource UpdatePersonnelEntity(this Personnel model, Resource entity)
        {
            entity.ResourceId = model.PersonnelId;
            entity.Available = model.Available;
            entity.Location = model.Location;
            return entity;
        }

        public static Personnel ToPersonnelModel(this Resource entity)
        {
            var model = new Personnel();
            return entity.UpdatePersonnelModel(model);
        }

        public static Personnel UpdatePersonnelModel(this Resource entity, Personnel model)
        {
            model.PersonnelId = entity.ResourceId;
            model.Available = entity.Available;
            model.Location = entity.Location;
            return model;
        }
    }
}
