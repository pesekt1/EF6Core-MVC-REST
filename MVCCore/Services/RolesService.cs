using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCCore.DbContext;
using MVCCore.Models;

namespace MVCCore.Services
{
    public class RolesService: ControllerBase
    {
        private readonly SchoolContext _context;
        
        public RolesService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
        
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(CreateRole), new { id = role.Id }, role);
        }
        
        public async Task<ActionResult<Role>> DeleteRole(long id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return role;
        }
        
        public async Task<ActionResult<Role>> UpdateRole(long id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return await _context.Roles.FindAsync(id);
        }
        
        private bool RoleExists(long id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}