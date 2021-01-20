using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sChallenge.Models;

namespace sChallenge.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }

        /*
         * Converts a User object to a User Data Transfer Object containing a 
         * subset of fields.
         */
        private static UserDTO UserToDTO(User user) => new UserDTO
        {
            Id = user.Id,
            Username = user.Username
        };

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUser()
        {
            return await _context.User.Select(user => UserToDTO(user)).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(long id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return UserToDTO(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = userDTO.Username;

            _context.Entry(userDTO).State = EntityState.Modified;

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

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO)
        {
            var user = new User
            {
                Username = userDTO.Username,
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, UserToDTO(user));
        }


        /*
         * POST: api/Users/login
         * Simulates login handling and returns a Json object verifying whether the operation was a success.
         * Upon success, the user's id will be returned. Upon failure, a User ID is returned along with a
         * failure flag.
         */
        [HttpPost("login")]
        public async Task<ActionResult> VerifyLogin(User postUser)
        {
            if(UserHasCredentials(postUser.Username,postUser.Password)) // credentials match
            {
                var userMatch = _context.User // Selects a single UserDTO whose username matches that which was provided
                    .Where(u => u.Username == postUser.Username)
                    .Select(u => UserToDTO(u))
                    .SingleOrDefaultAsync();

                return new JsonResult( new { success = true, userId = userMatch.Result.Id } ); // return user ID in JSON (can be stored for post-login operations)
            } else // credentials do not match
            {
                return new JsonResult( new { success = false } );
            }
        }

        /*
         * POST: api/Users/isSuper
         * Returns whether the User is a supervisor.
         */
        [HttpPost("isSuper")]
        public async Task<ActionResult> IsSupervisor(User u)
        {
            var user = await _context.User.FindAsync(u.Id);
            if (user == null)
            {
                return NotFound();
            }

            if (UserIsSuper(user)) // user is a supervisor
            {
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

        // DELETE: api/Users/5
        // Deletes a User from the database.
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user; // returns the deleted User object upon success
        }

        /*
         * Returns true/false whether the provided login credentials are correct (maps to an existing user)
         */
        private bool UserHasCredentials(string username, string password)
        {
            return _context.User.Any(user => user.Username == username && user.Password == password);
        }

        /*
         * Returns true/false whether the user exists in the database
         */
        private bool UserExists(long id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        /*
         * Returns true/false whether the user is a supervisor
         */
        private bool UserIsSuper(User user)
        {
            return _context.User.Any(u => u.Id == user.Id && u.IsSuper == true);
        }

    }
}
