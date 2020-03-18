using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication10.Models;
using WebApplication10.Models.ViewModel;

namespace WebApplication10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUsersController : ControllerBase
    {
        private readonly ChinaDBContext _context;
        public IConfiguration _configuration;
        public LoginUsersController(IConfiguration config, ChinaDBContext context)
        {
            _context = context;
            _configuration = config;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> PostLogin(LoginUsers _userData)
        {

            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUsers(_userData.Email, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserID.ToString()),                                      
                    new Claim("Email", user.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<LoginUsers> GetUsers(string email, string password)
        {
            return await _context.LoginUsers.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
        [AllowAnonymous]
        [HttpPost("PostLogin")]
        public async Task<tockenModel> Post(LoginUsers _userData)
        {
            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId",user.UserID.ToString()),
                    new Claim("Email",user.Email.ToString())
                  //  new Claim("",user.Username.ToString())
                   };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    tockenModel tc = new tockenModel
                    {
                        Tocken = new JwtSecurityTokenHandler().WriteToken(token),
                        UserID = user.UserID,
                        Username = user.Username,
                        Password = user.Password,
                        Email = user.Email,
                        AccountHoldersName = user.AccountHoldersName,
                        Accountnumber = user.Accountnumber,
                        IFSCCODE = user.IFSCCODE,
                        MobileNumber = user.MobileNumber,
                    };
                    return tc;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        private async Task<LoginUsers> GetUser(string email, string password)
        {
            return await _context.LoginUsers.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
        // GET: api/LoginUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginUsers>>> GetLoginUsers()
        {
            return await _context.LoginUsers.ToListAsync();
        }

        // GET: api/LoginUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginUsers>> GetLoginUsers(int id)
        {
            var loginUsers = await _context.LoginUsers.FindAsync(id);

            if (loginUsers == null)
            {
                return NotFound();
            }

            return loginUsers;
        }

        // PUT: api/LoginUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginUsers(int id, LoginUsers loginUsers)
        {
            if (id != loginUsers.UserID)
            {
                return BadRequest();
            }

            _context.Entry(loginUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginUsersExists(id))
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

        // POST: api/LoginUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LoginUsers>> PostLoginUsers(LoginUsers loginUsers)
        {
            _context.LoginUsers.Add(loginUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoginUsers", new { id = loginUsers.UserID }, loginUsers);
        }

        // DELETE: api/LoginUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoginUsers>> DeleteLoginUsers(int id)
        {
            var loginUsers = await _context.LoginUsers.FindAsync(id);
            if (loginUsers == null)
            {
                return NotFound();
            }

            _context.LoginUsers.Remove(loginUsers);
            await _context.SaveChangesAsync();

            return loginUsers;
        }

        private bool LoginUsersExists(int id)
        {
            return _context.LoginUsers.Any(e => e.UserID == id);
        }
    }
}
