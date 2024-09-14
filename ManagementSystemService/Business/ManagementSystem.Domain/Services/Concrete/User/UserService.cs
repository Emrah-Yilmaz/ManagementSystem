using AutoMapper;
using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;
using ManagementSystem.Domain.PasswordEncryptor;
using ManagementSystem.Domain.Persistence.User;
using ManagementSystem.Domain.Services.Abstract.User;
using ManagementSystem.Domain.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Packages.Exceptions.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagementSystem.Domain.Services.Concrete.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateUserArgs args, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Domain.Entities.User>(args);
            var hashedPass = Encrypt.Encript(args.PasswordHash);
            entity.PasswordHash = hashedPass;
            entity.Status = StatusType.Pending.ToString();
            var result = await _userRepository.AddAsync(entity, cancellationToken);

            //Todo
            //RabbitMQ implementasyonu sonrasında Doğrulama maili gönderilecek

            return result;
        }

        public async Task<LoginDto> LoginAsync(LoginArgs args, CancellationToken cancellationToken = default)
        {
            var dbUser = await _userRepository.SingleOrDefaultAsync(u => u.Email == args.Email);
            if (dbUser is null)
                return null;

            var hashedPass = Encrypt.Encript(args.Password);
            var isExistPass = dbUser.PasswordHash == hashedPass;
            if (!isExistPass)
            {
                throw new BusinessException("Kullanıcı adı ya da parola hatalı");
            }

            var model = new LoginDto
            {
                Id = dbUser.Id,
                FirstName = dbUser.Name,
                LastName = dbUser.LastName,
                UserName = dbUser.UserName,
            };

            var claims = new Claim[]
            {
                new Claim(Shared.JwtClaims.UserId, dbUser.Id.ToString()),
                new Claim(Shared.JwtClaims.Email, dbUser.Email),
                new Claim(Shared.JwtClaims.FirstName, dbUser.Name),
                new Claim(Shared.JwtClaims.LastName, dbUser.LastName),
                new Claim(Shared.JwtClaims.UserName, dbUser.UserName)
            };

            model.Token = GenerateToken(claims);

            return model;
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiry,
                signingCredentials: creds,
                notBefore: DateTime.Now
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
