using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCCore.Models;
using MVCCore.Services;

namespace MVCCore.Rest_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolesService _rolesService;

        public RolesController(RolesService rolesService)
        {
            _rolesService = rolesService;
        }

        // GET: api/roles
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _rolesService.GetRoles();
        }
        
        // PUT: api/roles/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole(long id, Role role)
        {
            return await _rolesService.UpdateRole(id, role);
        }
        
        // POST: api/roles
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            return await _rolesService.CreateRole(role);
        }
        
        // DELETE: api/roles/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> DeleteRole(long id)
        {
            return await _rolesService.DeleteRole(id);
        }
    }
}