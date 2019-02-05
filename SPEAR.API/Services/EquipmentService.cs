using SPEAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPEAR.Data.Entities;
using SPEAR.Data.Repositories;
using SPEAR.Models;
using SPEAR.Services.Mappers;

namespace SPEAR.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IResourceRepository _repository;

        public EquipmentService(IResourceRepository repository)
        {
            _repository = repository;
        }

        public List<Equipment> Get()
        {
            var resources = _repository.Get()
                .Where(x => x.IsActive && !x.IsDeleted);

            var models = resources.Select(x => x.ToEquipmentModel()).ToList();
            return models;
        }

        public Equipment Get(Guid? equipmentId)
        {
            if (equipmentId.HasValue && equipmentId != Guid.Empty)
            {
                var model = _repository.Get(equipmentId.Value)?.ToEquipmentModel();
                if (model == null)
                {
                    throw new KeyNotFoundException();  //If Equipment ID was specified but not found, throw exception
                }

                return model;
            }
            return Create();
        }

        public async Task<Equipment> Create(Equipment model)
        {
            model.EquipmentId = model.EquipmentId == Guid.Empty ? Guid.NewGuid() : model.EquipmentId;

            var entity = _repository.Get(model.EquipmentId);
            if (entity != null)
            {
                return await Update(model, entity);
            }

            //TODO: Created by Organization
            entity = model.ToEquipmentEntity();
            _repository.Create(entity);
            return entity.UpdateEquipmentModel(model);
        }

        public async Task<Equipment> Update(Equipment model)
        {
            var entity = _repository.Get(model.EquipmentId);

            return await Update(model, entity);
        }
        private async Task<Equipment> Update(Equipment model, Resource entity)
        {
            if (entity == null)
            {
                throw new KeyNotFoundException();
            }

            entity = model.UpdateEquipmentEntity(entity);
            _repository.Update(entity);

            return entity.UpdateEquipmentModel(model);
        }

        public void Delete(Guid equipmentId)
        {
            var entity = _repository.Get(equipmentId);
            if (entity == null)
            {
                throw new KeyNotFoundException();  //If Resource ID was specified but not found, we cant update a non existing entity
            }

            entity.IsDeleted = true;
            _repository.Update(entity);
        }

        private Equipment Create()
        {
            return new Equipment
            {
                EquipmentId = Guid.NewGuid(),
                Location = Location.Unknown,
                Available = false
            };
        }
    }
}
