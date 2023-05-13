using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelManagement.Data;
using TravelManagement.Models.Dto;
using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public class AuthRepository:IAuthRepository
    {
        private readonly ApplicationDbContext db;
        public string secretKey;
        public AuthRepository(ApplicationDbContext _db, IConfiguration configuration)
        {
            db = _db;
            secretKey = configuration.GetValue<String>("ApiSettings:Secret");



        }

        public bool IsUniqueUser(string username)
        {
            var user = db.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto)
        {
            var user = db.LocalUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower() &&
            x.Password == loginRequestDto.Password);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(TokenDescriptor);
            LoginResponseDTO loginResponseDto = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };



            return loginResponseDto;
        }

        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDto)
        {
            LocalUser user = new LocalUser()
            {
                UserName = registrationRequestDto.UserName,
                Password = registrationRequestDto.Password,
                Name = registrationRequestDto.Name,
                Role = registrationRequestDto.Role

            };
            db.LocalUsers.Add(user);
            await db.SaveChangesAsync();
            user.Password = "";
            return user;



        }
    }
}
