using Microsoft.EntityFrameworkCore;
using ServerAPI.Models;
using ServerAPI.Models.Context;
using ServerAPI.Services.Interface;

namespace ServerAPI.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly AppDbContext _appDbContext;

        public UserLoginService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<AppUser> RegisterUser(AppUser user)
        {
            if (_appDbContext.Users.Any(u => u.UserName == user.UserName))
                return null;
            else
            {
                await _appDbContext.Users.AddAsync(user);
                await _appDbContext.SaveChangesAsync();
                return user;
            }
        }

        public async Task<bool> RemoveUser(string userName)
        {
            if (!_appDbContext.Users.Any(u => u.UserName == userName))
                return false;
            else
            {
                _appDbContext.Users.Remove(await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName));
                await _appDbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<AppUser> UpdateUser(string userName, AppUser user)
        {
            if (!_appDbContext.Users.Any(u => u.UserName == userName))
                return null;
            else
            {
                var oldUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                oldUser = user;
                await _appDbContext.SaveChangesAsync();
                return user;
            }
        }
    }
}
