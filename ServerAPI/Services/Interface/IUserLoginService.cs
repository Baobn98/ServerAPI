using ServerAPI.Models;

namespace ServerAPI.Services.Interface
{
    public interface IUserLoginService
    {
        public Task<AppUser> RegisterUser (AppUser user);
        public Task<bool> RemoveUser(string userName);
        public Task<AppUser> UpdateUser (string userName, AppUser user);

    }
}
