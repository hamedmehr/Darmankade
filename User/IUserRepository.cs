using Darmankade.Contract;
using Darmankade.Model.ViewModels;
using System;
using System.Collections.Generic;

namespace User
{
    public interface IUserRepository<TEntity> : IRepository<TEntity>
    {
        UserViewModel GetUser(Guid ID);
        IEnumerable<UserViewModel> GetUsers();
        bool AddUser(UserViewModel userViewModel);
    }
}
