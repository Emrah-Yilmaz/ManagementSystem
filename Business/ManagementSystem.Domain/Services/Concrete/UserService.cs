﻿using AutoMapper;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.PasswordEncryptor;
using ManagementSystem.Domain.Persistence;
using ManagementSystem.Domain.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagementSystem.Domain.Services.Concrete
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
            var entity = _mapper.Map<User>(args);
            var hashedPass = Encrypt.Encript(args.PasswordHash);
            entity.PasswordHash = hashedPass;
            var result = await _userRepository.AddAsync(entity);

            return result;
        }

        public async Task<LoginDto> LoginAsync(LoginArgs args, CancellationToken cancellationToken = default)
        {
            var dbUser = await _userRepository.SingleOrDefaultAsync(u => u.Email == args.Email);

            if (dbUser is null)
                return null;

            var model = new LoginDto
            {
                Id = dbUser.Id,
                FirstName = dbUser.Name,
                LastName = dbUser.LastName,
                UserName = dbUser.UserName,
            };

            var claims = new Claim[]
            {
                new Claim("userId", dbUser.Id.ToString()),
                new Claim("mailAddress", dbUser.Email),
                new Claim("firstName", dbUser.Name),
                new Claim("lastName", dbUser.LastName),
                new Claim("username", dbUser.UserName)
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
