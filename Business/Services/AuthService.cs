using AutoMapper;
using Business.Exceptions;
using Business.Handlers;
using Business.Interfaces;
using Business.Models.Auth;
using Data.Entities;
using Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper; 

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Required fields are not filled");

            var userAccount = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (userAccount == null)
                throw new ArgumentException($"No user found with email: {request.Email}");

            if (!PasswordHashHandler.VerifyPassword(request.Password, userAccount.Password))
                throw new ArgumentException("Incorrect password");

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = int.Parse(_configuration["JwtConfig:TokenValidityMins"]);
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, request.Email)
                }),
                Expires = tokenExpiryTimeStamp,
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                Username = userAccount.Username,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }

        public async Task Register(RegistrationRequestDto request)
        {
            // TODO: add model validation
            var userInDb = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (userInDb != null)
            {
                Console.WriteLine("блять");
                throw new MusicLibraryException("User already exists!");
            }

            // TODO: add password hashing
            var user = _mapper.Map<User>(request);
            user.Id = Guid.NewGuid();
            user.Password = PasswordHashHandler.HashPassword(request.Password);
            user.IsBlocked = false;
            user.IsAdmin = false;
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}