using Darmankade.Application;
using Darmankade.Model.Models;
using Darmankade.Model.ViewModels;
using Darmankade.ServiceBroker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
namespace User
{
    public class UserRepository : Repository<Darmankade.Model.Models.User>, IUserRepository<Darmankade.Model.Models.User>
    {
        public UserRepository(UserDBContext dBContext) : base(dBContext)
        {
        }

        public bool AddUser(UserViewModel userViewModel)
        {
            var user = new Darmankade.Model.Models.User()
            {
                Name = userViewModel.Name,
                LastName = userViewModel.LastName
            };
            return this.Cretae(user) != null;
        }

        public UserViewModel GetUser(Guid ID)
        {
            var user = this.GetByID(ID);
            return new UserViewModel() { Name = user.Name, LastName = user.LastName };
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = this.GetAll();
            var userViewModels = new List<UserViewModel>();
            users.ToList().ForEach(user =>
            {
                userViewModels.Add(new UserViewModel() { Name = user.Name, LastName = user.LastName });
            });
            return userViewModels;
        }
    }
}
