using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TalentPlus.Application.DTOs.Employee;
using TalentPlus.Application.Interfaces;
using TalentPlus.Infrastructure.Email;

namespace TalentPlus.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<IdentityUser> userManager,
                           SignInManager<IdentityUser> signInManager,
                           IEmailService emailService,
                           string jwtKey,
                           string jwtIssuer,
                           string jwtAudience)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _jwtKey = jwtKey;
            _jwtIssuer = jwtIssuer;
            _jwtAudience = jwtAudience;
        }
        
        public async Task<EmployeeDto> RegisterAsync(RegisterEmployeeDto dto)
        
        {
            Console.WriteLine("EMAIL: '" + dto.Email + "'");
            Console.WriteLine("PASSWORD: '" + dto.Password + "'");
            Console.WriteLine("FIRSTNAME: '" + dto.FirstName + "'");
            Console.WriteLine("LASTNAME: '" + dto.LastName + "'");
            Console.WriteLine("DOCUMENT: '" + dto.Document + "'");
            Console.WriteLine("PHONE: '" + dto.Phone + "'");
            Console.WriteLine("ADDRESS: '" + dto.Address + "'");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email is required", nameof(dto.Email));

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password is required", nameof(dto.Password));

            if (string.IsNullOrWhiteSpace(dto.FirstName))
                throw new ArgumentException("FirstName is required", nameof(dto.FirstName));

            if (string.IsNullOrWhiteSpace(dto.LastName))
                throw new ArgumentException("LastName is required", nameof(dto.LastName));

            if (string.IsNullOrWhiteSpace(dto.Document))
                throw new ArgumentException("Document is required", nameof(dto.Document));
            
            var user = new IdentityUser
            {
                UserName = dto.Email.Trim(),
                Email = dto.Email.Trim(),
                PhoneNumber = string.IsNullOrWhiteSpace(dto.Phone) ? null : dto.Phone.Trim()
            };
            
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            
            await _emailService.SendEmailAsync(dto.Email, "Welcome to TalentPlus", "Your registration was successful!");
            
            return new EmployeeDto
            {
                Id = 0, 
                Document = dto.Document,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };
        }
        
        public async Task<string> LoginAsync(LoginEmployeeDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Invalid credentials");
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Invalid credentials");
            
            var roles = await _userManager.GetRolesAsync(user);
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
