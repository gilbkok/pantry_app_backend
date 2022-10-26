using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pantryBackend.Data;
using pantryBackend.Models;
using System.Reflection.Metadata.Ecma335;

namespace pantryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly pantryDbContext _UsersContext;
        public UsersController(pantryDbContext usersContext)
        {
            _UsersContext = usersContext;
        }

        [HttpGet]

        public ActionResult<IEnumerable<Users>> GetAllUsers()
        {
            return _UsersContext.users;
        }
        [HttpPost]

        public void PostUser(Users user)
        {
            _UsersContext.users.Add(user);

            _UsersContext.SaveChanges();

        }
        [HttpGet]
        [Route("{uid}")]

        public async Task<IActionResult> GetUserbyId([FromRoute] int uid)
        {
            var user = await _UsersContext.users.FirstOrDefaultAsync(x => x.uid == uid);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("pantry item not found");
        }

        


    }
}
