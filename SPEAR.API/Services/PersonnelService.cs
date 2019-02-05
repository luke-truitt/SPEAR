using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPEAR.Data.Entities;
using SPEAR.Data.Enums;
using SPEAR.Data.Repositories;
using SPEAR.Models;
using SPEAR.Services.Mappers;

namespace SPEAR.Services
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IResourceRepository _repository;

        public PersonnelService(IResourceRepository repository)
        {
            _repository = repository;
        }

        public List<Personnel> Get()
        {
            var resources = _repository.Get()
                .Where(x => x.IsActive && !x.IsDeleted);
            
            var models = resources.Select(x => x.ToPersonnelModel()).ToList();
            return models;
        }

        public Personnel Get(Guid? personnelId)
        {
            if (personnelId.HasValue && personnelId != Guid.Empty)
            {
                var model = _repository.Get(personnelId.Value)?.ToPersonnelModel();
                if (model == null)
                {
                    throw new KeyNotFoundException();  //If Personnel ID was specified but not found, throw exception
                }

                return model;
            }
            return Create();
        }

        public async Task<Personnel> Create(Personnel model)
        {
            model.PersonnelId = model.PersonnelId == Guid.Empty ? Guid.NewGuid() : model.PersonnelId;
         
            var entity = _repository.Get(model.PersonnelId);
            if (entity != null)
            {
                return await Update(model, entity);
            }

            //TODO: Created by Organization
            entity = model.ToPersonnelEntity();
            _repository.Create(entity);
            return entity.UpdatePersonnelModel(model);
        }

        public async Task<Personnel> Update(Personnel model)
        {
            var entity = _repository.Get(model.PersonnelId);
            
            return await Update(model, entity);
        }
        private async Task<Personnel> Update(Personnel model, Resource entity)
        {
            if (entity == null)
            {
                throw new KeyNotFoundException();
            }

            entity = model.UpdatePersonnelEntity(entity);
            _repository.Update(entity);

            return entity.UpdatePersonnelModel(model);
        }

        public void Delete(Guid personnelId)
        {
            var entity = _repository.Get(personnelId);
            if (entity == null)
            {
                throw new KeyNotFoundException();  //If Resource ID was specified but not found, we cant update a non existing entity
            }

            entity.IsDeleted = true;
            _repository.Update(entity);
        }
        
        private Personnel Create()
        {
            return new Personnel
            {
                PersonnelId = Guid.NewGuid(),
                Location = Location.Unknown,
                Available = false
            };
        }
    }
}
