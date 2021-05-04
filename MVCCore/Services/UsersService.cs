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
    public class UsersService: ControllerBase
    {
        private readonly SchoolContext _context;
        
        public UsersService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            user.Password = Utils.EncryptData(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }
        
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
        
        public async Task<ActionResult<User>> UpdateUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return await _context.Users.FindAsync(id);
        }
        
        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}