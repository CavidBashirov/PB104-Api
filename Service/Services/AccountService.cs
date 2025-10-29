using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.DTOs.Account;
using Service.Helpers.Enums;
using Service.Helpers.Exceptions;
using Service.Helpers.Responses;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMapper _mapper;
        public AccountService(UserManager<AppUser> userManager,
                              IMapper mapper,
                              RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<ServiceResponse> AddRoleToUserAsync(UserRoleDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) throw new NotFoundException();

            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null) throw new NotFoundException();

            if(await _userManager.IsInRoleAsync(user, role.Name))
            {
                return new ServiceResponse { IsSuccess = false, Messages = ["This role already exists in user"]  };
            }

            await _userManager.AddToRoleAsync(user, role.Name);

            return new ServiceResponse { IsSuccess = true, Messages = ["Role added successfully"] };
        }

        public async Task CreateRolesAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if(!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public async Task<ServiceResponse> DeleteRoleFromUserAsync(UserRoleDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) throw new NotFoundException();
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null) throw new NotFoundException();
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Messages = ["This role is exist"]
                };
            }
            await _userManager.RemoveFromRoleAsync(user, role.Name);
            ServiceResponse serviceResponse = new ServiceResponse()
            {
                IsSuccess = true,
                Messages = ["This role deleted successfully"]
            };
            return serviceResponse;
          

        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) throw new NotFoundException();
            await _userManager.DeleteAsync(user);
        }

        public async Task<RoleDto> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) throw new NotFoundException();
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) throw new NotFoundException();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var mappedUsers = _mapper.Map<IEnumerable<UserDto>>(users);

            foreach (var user in mappedUsers)
            {
                var userEntity = await _userManager.FindByIdAsync(user.Id);
                var roles = await _userManager.GetRolesAsync(userEntity);
                user.Roles = roles.ToArray();
            }
            return mappedUsers;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterDto model)
        {
            AppUser user = new()
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponse
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(m=>m.Description).ToList()
                };
            }

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            return new RegisterResponse
            {
                IsSuccess = true,
                Errors = null
            };
        }
    }
}
