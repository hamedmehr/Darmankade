using Darmankade.Contract;
using Darmankade.Model.Models;
using Darmankade.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.v1.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        public IAuthRepository<Auth> AuthRepository { get; set; }
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthRepository<Auth> authRepository)
        {
            AuthRepository = authRepository;
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<bool> AddUser([FromBody] UserViewModel userViewModel)
        {
            return await Task.Factory.StartNew(() =>
            {
                return AuthRepository.AddAuth(userViewModel);
            });
        }
    }
}
