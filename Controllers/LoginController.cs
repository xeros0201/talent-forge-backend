using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TFBackend.Data;
using TFBackend.Entities.Dto.Login;
using TFBackend.Entities.Dto.Staff;
using TFBackend.Models;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace TFBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public LoginController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        // POST: api/login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostLogin(LoginPostDto loginPostDto)
        {
            //create new staff
            var user = await _context.Staff.FirstOrDefaultAsync(e => e.Username == loginPostDto.Username);
            if (user == null)
            {
                return CustomResult("Not found", System.Net.HttpStatusCode.NotFound);
            }

            var verify = HashPassword.VerifyHash(loginPostDto.Password, user.Password);
            if (!verify)
            {
                return CustomResult("Username or password incorrect", System.Net.HttpStatusCode.BadRequest);
            }
            var role = await _context.Roles.FirstOrDefaultAsync(e => e.Id == user.RoleId);


            return CustomResult("Success", new LoginResponse
            {
                User = new StaffDto
                {
                    Username = user.Username,
                    Name = user.Username,
                    Available = user.Available,
                    Id = user.Id,
                    Picture = user.Picture,
                    Role = role.Name,
                    AvailableDate = user.AvailableDate,
                },
                accessToken = GetToken(user),
                refreshToken = GetRefreshToken(user),

            }
            );

        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetToken(Staff staff)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", staff.Id.ToString()),
                        new Claim("Name", staff.Name),
                        new Claim("Role", staff.RoleId.ToString())
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: signIn);


            string Token = new JwtSecurityTokenHandler().WriteToken(token);

            return Token;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetRefreshToken(Staff staff)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", staff.Id.ToString()),
                        new Claim("Name", staff.Name),
                        new Claim("Role", staff.RoleId.ToString())
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:KeyRF"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: signIn);


            string Token = new JwtSecurityTokenHandler().WriteToken(token);

            return Token;
        }
    }
}



