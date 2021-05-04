using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCCore.Models;
using MVCCore.Services;

namespace MVCCore.Rest_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _usersService.GetUsers();
        }
        
        // PUT: api/users/1
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(long id, User user)
        {
            return await _usersService.UpdateUser(id, user);
        }
        
        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            return await _usersService.CreateUser(user);
        }
        
        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            return await _usersService.DeleteUser(id);
        }
    }
}