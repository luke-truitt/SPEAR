using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPEAR.Data.Entities;
using SPEAR.Models;
using Type = SPEAR.Data.Enums.Type;

namespace SPEAR.Services.Mappers
{
    public static class EquipmentMapper
    {
        public static Data.Entities.Resource ToEquipmentEntity(this Equipment model)
        {
            var entity = new Resource
            {
                IsActive = true,
                IsDeleted = false,
                ResourceType = Type.Equipment
            };
            return model.UpdateEquipmentEntity(entity);
        }

        public static Data.Entities.Resource UpdateEquipmentEntity(this Equipment model, Resource entity)
        {
            entity.ResourceId = model.EquipmentId;
            entity.Available = model.Available;
            entity.Location = model.Location;
            return entity;
        }

        public static Equipment ToEquipmentModel(this Resource entity)
        {
            var model = new Equipment();
            return entity.UpdateEquipmentModel(model);
        }

        public static Equipment UpdateEquipmentModel(this Resource entity, Equipment model)
        {
            model.EquipmentId = entity.ResourceId;
            model.Available = entity.Available;
            model.Location = entity.Location;
            return model;
        }
    }
}
