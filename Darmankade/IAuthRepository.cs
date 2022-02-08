using Darmankade.Contract;
using Darmankade.Model.Models;
using Darmankade.Model.ViewModels;

namespace Authentication
{
    public interface IAuthRepository<TEntity> : IRepository<TEntity>
    {
        bool AddAuth(UserViewModel userViewModel);
    }
}
