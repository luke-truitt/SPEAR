using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPEAR.Data.Entities;
using SPEAR.Models;
using SPEAR.Services;

namespace SPEAR.Controllers
{
    [Produces("application/json")]
    public class PersonnelController : Controller
    {
        private readonly IPersonnelService _service;

        public PersonnelController(IPersonnelService service)
        {
            _service = service;
        }

        [HttpGet("personnel/{personnelId}")]
        public IActionResult Get(Guid? personnelId)
        {
            var model = _service.Get(personnelId);
            return Ok(model);
        }

        //TODO: Move non-activate resource cards to another controller. This controller needs to be open to unauthenticated users
        [HttpGet("personnel")]
        public IActionResult Get()
        {
            var models = _service.Get();
            return Ok(models);
        }

        [HttpPost("personnel")]
        public async Task<IActionResult> Post([FromBody]Personnel model)
        {
            var response = await _service.Create(model);
            return Ok(response);
        }

        [HttpPut("personnel/{personnelId}")]
        public void Put(Guid resourceId, [FromBody]Resource model)
        {
            throw new NotImplementedException("This REST protocal is not implemented, use the POST feature");
        }

        [HttpDelete("personnel/{personnelId}")]
        public IActionResult Delete(Guid personnelId)
        {
            if (personnelId == Guid.Empty)
            {
                return BadRequest(ModelState);
            }

            //TODO: Make sure this dashboard belings to this portal
            _service.Delete(personnelId);
            return Ok();
        }
    }
}
