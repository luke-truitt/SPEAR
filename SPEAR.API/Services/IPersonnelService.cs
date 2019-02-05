using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPEAR.Data.Entities;
using SPEAR.Models;

namespace SPEAR.Services
{
    public interface IPersonnelService
    {
        Personnel Get(Guid? personnelId);
        List<Personnel> Get();
        Task<Personnel> Create(Personnel model);
        Task<Personnel> Update(Personnel model);

        void Delete(Guid personnelId);
    }
}
