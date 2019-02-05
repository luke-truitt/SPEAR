using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPEAR.Models;

namespace SPEAR.Services
{
    public interface IEquipmentService
    {
        Equipment Get(Guid? equipmentId);
        List<Equipment> Get();
        Task<Equipment> Create(Equipment model);
        Task<Equipment> Update(Equipment model);

        void Delete(Guid equipmentId);
    }
}
