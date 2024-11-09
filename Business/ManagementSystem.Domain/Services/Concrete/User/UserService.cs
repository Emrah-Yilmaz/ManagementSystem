using AutoMapper;
using Bogus;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;
using ManagementSystem.Domain.PasswordEncryptor;
using ManagementSystem.Domain.Persistence.Comment;
using ManagementSystem.Domain.Persistence.Department;
using ManagementSystem.Domain.Persistence.Location;
using ManagementSystem.Domain.Persistence.User;
using ManagementSystem.Domain.Services.Abstract.User;
using ManagementSystem.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Packages.Exceptions.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ManagementSystem.Domain.Services.Concrete.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IAddressRepository _addressRepository;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, IDepartmentRepository departmentRepository, IProjectRepository projectRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _projectRepository = projectRepository;
            _addressRepository = addressRepository;
        }

        public async Task<bool> AddUserToDepartment(AddUserToDepartmentArgs args, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(args.UserId);

            if (user is null)
                return default;
            
            var department = await _departmentRepository.GetByIdAsync(args.DepartmentId);
            if (department is null)
                return default;

            user.DepartmentId = department.Id;

            var result = await _userRepository.UpdateAsync(user, cancellationToken);
            if (result == 0)
            {
                throw new Exception();
            }

            return true;
        }

        public async Task<bool> AssignUserToProjectAsync(AssignUserToProjectArgs args, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FirstOrDefaultAsync(
                predicate: u => u.Id == args.UserId,
                noTracking: false,
                cancellationToken: default,
                includes: p => p.Projects);
                

            if (user is null)
                return false;

            var project = await _projectRepository.FirstOrDefaultAsync(
                predicate: p => p.Id == args.ProjectId,
                noTracking: false,
                cancellationToken: default,
                includes: u => u.Users);

            if (project is null)
                return false;

            user.Projects.Add(project);
            project.Users.Add(user);
            var result = await _userRepository.SaveChangeAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> ChangeStatusAsync(ChangeStatusArgs args, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(
                id: args.Id,
                noTracking: false,
                cancellationToken: default);

            if (user is null)
                throw new Exception("User not found");

            user.Status = args.Status.ToString();

            var result = await _userRepository.SaveChangeAsync(cancellationToken);

            return result > 0;
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

        public async Task<bool> CreateUserAddressAsync(CreateAddressArgs args, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(args.UserId);
            if (user is null)
                return false;

            var userAddress = new Address()
            {
                CityId = args.CityId,
                DistrictId = args.DistrictId,
                QuerterId = args.QuarterId,
                UserId = args.UserId,
                Description = args.Description,
                Status = StatusType.Published.ToString()
            };
            user.Addresses ??= new List<Address>();
            user.Addresses.Add(userAddress);
            await _userRepository.UpdateAsync(user, cancellationToken);
            return true;
        }

        public async Task<bool> CreateUsersWithBogus(CancellationToken cancellationToken = default)
        {

            var faker = new Faker<Domain.Entities.User>("tr")
                        .RuleFor(u => u.Name, f => f.Name.FirstName())
                        .RuleFor(u => u.LastName, f => f.Name.LastName())
                        .RuleFor(u => u.UserName, (f, u) => $"{u.Name.ToLower()}.{u.LastName.ToLower()}")
                        .RuleFor(u => u.Email, f => f.Internet.Email())
                        .RuleFor(u => u.Status, StatusType.Published.ToString())
                        .RuleFor(u => u.PasswordHash, f => f.Internet.Password());

            var users = faker.Generate(100);
            var saved = await _userRepository.AddRangeAsync(users);
            if (saved == 0)
                return false;

            var departmentFaker = new Faker<Domain.Entities.Department>("tr")
                            .RuleFor(d => d.Name, f => f.Commerce.Department()) // Rastgele bir departman adı
                            .RuleFor(d => d.Users, f =>
                            {
                                // Kullanıcıları rastgele seçmek için int kullanıcı id'sini kullan
                                var userCount = users.Count;
                                var random = new Random();
                                return Enumerable.Range(0, 5) // 5 kullanıcı seç
                                                 .Select(_ => users[random.Next(userCount)]) // Rastgele kullanıcı seç
                                                 .Distinct() // Tekrar eden kullanıcıları kaldır
                                                 .ToList();
                            })
                            .RuleFor(d => d.Projects, f => new List<Project>()); // Projeler boş bir liste

            var departments = departmentFaker.Generate(15);
            var savedDepartments = await _departmentRepository.AddRangeAsync(departments);

            
            var projectFaker = new Faker<Project>("tr")
                            .RuleFor(p => p.Name, f => f.Commerce.ProductName()) // Rastgele bir proje adı
                            .RuleFor(p => p.Users, f =>
                            {
                                // Kullanıcılardan rastgele 10 kullanıcı seç
                                var random = new Random();
                                return Enumerable.Range(0, 10)
                                                 .Select(_ => users[random.Next(users.Count)]) // Rastgele kullanıcı seç
                                                 .Distinct() // Tekrar eden kullanıcıları kaldır
                                                 .ToList();
                            })
                            .RuleFor(p => p.Departments, f =>
                            {
                                // Departmanlardan rastgele 2 departman seç
                                var random = new Random();
                                return Enumerable.Range(0, 2)
                                                 .Select(_ => departments[random.Next(departments.Count)]) // Rastgele departman seç
                                                 .Distinct() // Tekrar eden departmanları kaldır
                                                 .ToList();
                            })
                            .RuleFor(p => p.WorkTasks, f => new List<Domain.Entities.WorkTask>()); // Çalışma görevleri boş bir liste

                                    // 10 adet proje oluşturma
                                    var projects = projectFaker.Generate(10);

            var savedProjects = await _projectRepository.AddRangeAsync(projects);


            return true;
        }

        public async Task<UserDto> GetUser(int userId, CancellationToken cancellationToken = default)
        {
            var user = _userRepository.GetThenInclude(
                       predicate: u => u.Id == userId,
                        noTracking: true,
                        includes: new Func<IQueryable<Entities.User>, IQueryable<Entities.User>>[]
                        {
                            q => q.Include(d => d.Department).ThenInclude(p => p.Projects),
                            q => q.Include(u => u.Addresses), // User'dan Addresses'e geçiş
                            q => q.Include(u => u.Addresses).ThenInclude(a => a.City),// Address'ten City'e geçiş
                            q => q.Include(u => u.Addresses).ThenInclude(a => a.District), // Address'ten City'e geçiş
                            q => q.Include(u => u.Addresses).ThenInclude(a => a.Quarter) // Address'ten City'e geçiş
                        }).FirstOrDefault();
            if (user is null)
                return null;

            var adrresses = _addressRepository.Get(p => p.UserId == userId, false, c => c.City, d => d.District, q => q.Quarter);
            var mappedResult =  _mapper.Map<UserDto>(user);
            return mappedResult;
        }

        public async Task<List<UserDto>> GetUsers(GetUserArgs args, CancellationToken cancellationToken = default)
        {
            var includes  = new Func<IQueryable<Entities.User>, IQueryable<Entities.User>>[]{ };
            var users = new List<Entities.User>();

            if (args.UserRequestType == UserRequestType.Basic)
            {
                users = await _userRepository.GetListAsync(predicate: null,
                            noTracking: false,
                            orderBy: null,
                            includes: null);
                return Map(users);
            }
            else if (args.UserRequestType == UserRequestType.Address)
            {
                includes = new Func<IQueryable<Entities.User>, IQueryable<Entities.User>>[]
                {
                    q => q.Include(u => u.Addresses), // User'dan Addresses'e geçiş
                    q => q.Include(u => u.Addresses).ThenInclude(a => a.City),// Address'ten City'e geçiş
                    q => q.Include(u => u.Addresses).ThenInclude(a => a.District), // Address'ten City'e geçiş
                    q => q.Include(u => u.Addresses).ThenInclude(a => a.Quarter) // Address'ten City'e geçiş
                };
            }
            else
            {
                includes = new Func<IQueryable<Entities.User>, IQueryable<Entities.User>>[]
                            {
                            q => q.Include(d => d.Department).ThenInclude(p => p.Projects),
                            q => q.Include(u => u.Addresses), // User'dan Addresses'e geçiş
                            q => q.Include(u => u.Addresses).ThenInclude(a => a.City),// Address'ten City'e geçiş
                            q => q.Include(u => u.Addresses).ThenInclude(a => a.District), // Address'ten City'e geçiş
                            q => q.Include(u => u.Addresses).ThenInclude(a => a.Quarter) // Address'ten City'e geçiş
                            };
            }

            users = _userRepository.GetThenInclude(
                             predicate: null,
                             noTracking: true,
                             includes: includes).ToList();

            return Map(users);
        }

        private List<UserDto> Map(List<Domain.Entities.User> users)
        {
            if (users is null || users.Count == 0)
                return null;

            var mappedResult = _mapper.Map<List<UserDto>>(users);
            return mappedResult;
        }
        public async Task<LoginDto> LoginAsync(LoginArgs args, CancellationToken cancellationToken = default)
        {
            var dbUser =  _userRepository.GetThenInclude(u => u.UserName == args.UserName, false, q => q.Include(ur => ur.UserRoles).ThenInclude(role => role.Role)).FirstOrDefault();
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

            var userRole = dbUser.UserRoles?.Select(p => p.Role.Name)?.FirstOrDefault() ?? Roles.None.ToString();
            var claims = new Claim[]
            {

                new Claim(Shared.JwtClaims.UserId, dbUser.Id.ToString()),
                new Claim(Shared.JwtClaims.Email, dbUser.Email),
                new Claim(Shared.JwtClaims.FirstName, dbUser.Name),
                new Claim(Shared.JwtClaims.LastName, dbUser.LastName),
                new Claim(Shared.JwtClaims.UserName, dbUser.UserName),
                new Claim(Shared.JwtClaims.Role, userRole ?? null)
            };

            model.Token = GenerateToken(claims);

            return model;
        }

        public async Task<int> UpdateUserAddressAsync(UpdateAddressArgs args, CancellationToken cancellationToken = default)
        {
            var address = await _addressRepository.GetByIdAsync(args.Id);
            if (address is null)
                return default;
            
            var entity = args.Modify(address);
            var result = await _addressRepository.UpdateAsync(entity, cancellationToken);
            if (result == 0)
                return default;

            return result;
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
