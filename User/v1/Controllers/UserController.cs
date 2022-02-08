using Darmankade.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.v1.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        public IUserRepository<Darmankade.Model.Models.User> UserRepository { get; set; }
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository<Darmankade.Model.Models.User> userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpGet]
        [Route("GetUsers")]
        public IEnumerable<UserViewModel> GetGetUsers()
        {
            return UserRepository.GetUsers();
        }
    }
}
