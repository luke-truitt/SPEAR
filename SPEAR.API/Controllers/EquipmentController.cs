using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPEAR.Data.Entities;
using SPEAR.Models;
using SPEAR.Services;

namespace SPEAR.Controllers
{
    [Produces("application/json")]
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _service;

        public EquipmentController(IEquipmentService service)
        {
            _service = service;
        }

        [HttpGet("equipment/{equipmentId}")]
        public IActionResult Get(Guid? equipmentId)
        {
            var model = _service.Get(equipmentId);
            return Ok(model);
        }

        //TODO: Move non-activate resource cards to another controller. This controller needs to be open to unauthenticated users
        [HttpGet("equipment")]
        public IActionResult Get()
        {
            var models = _service.Get();
            return Ok(models);
        }
        
        [HttpPost("equipment")]
        public async Task<IActionResult> Post([FromBody]Equipment model)
        {
            var response = await _service.Create(model);
            return Ok(response);
        }
        
        [HttpPut("equipment/{equipmentId}")]
        public void Put(Guid resourceId, [FromBody]Resource model)
        {
            throw new NotImplementedException("This REST protocal is not implemented, use the POST feature");
        }

        [HttpDelete("equipment/{equipmentId}")]
        public IActionResult Delete(Guid equipmentId)
        {
            if (equipmentId == Guid.Empty)
            {
                return BadRequest(ModelState);
            }

            //TODO: Make sure this dashboard belings to this portal
            _service.Delete(equipmentId);
            return Ok();
        }
    }
}
