using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User
{
    public class UserAdd
    {
        public IUserRepository<Darmankade.Model.Models.User> UserService { get; set; }
        public UserAdd(IUserRepository<Darmankade.Model.Models.User> userService)
        {
            UserService = userService;
        }
    }
}
