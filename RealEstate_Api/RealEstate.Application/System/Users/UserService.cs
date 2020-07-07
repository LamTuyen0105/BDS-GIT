using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Application.Common;
using RealEstate.Data.EF;
using RealEstate.Data.Entity;
using RealEstate.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _config;
        private readonly RealEstateDbContext _context;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, IStorageService storageService, IConfiguration config, RealEstateDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _storageService = storageService;
            _config = config;
            _context = context;
        }

        public async Task<string> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            var query = from u in _context.Users
                        select new { u };
            var data = await query.Select(x => new UserViewModel()
            {
                Id = x.u.Id,
                Avatar = x.u.Avatar,
                FullName = x.u.FullName,
                Email = x.u.UserName,
                Gender = x.u.Gender,
                IdentityNumber = x.u.IdentityNumber,
                Address = x.u.Address                
            }).ToListAsync();
            return data;
        }

        public async Task<UserViewModel> GetById(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Avatar = user.Avatar,
                FullName = user.FullName,
                Email = user.UserName,
                Gender = user.Gender,
                IdentityNumber = user.IdentityNumber,
                Address = user.Address
            };
            return userViewModel;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new AppUser()
            {
                FullName = request.FullName,
                Gender = request.Gender,
                Address = request.Address,
                IdentityNumber = request.IdentityNumber,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            if (request.ThumbnailImage != null)
            {
                user.Avatar = await this.SaveFile(request.ThumbnailImage);
            }
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}