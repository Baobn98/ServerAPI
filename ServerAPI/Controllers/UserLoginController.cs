using Microsoft.AspNetCore.Mvc;
using ServerAPI.Models.Context;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserLoginController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


    }
}
