using Darmankade.Application;
using Darmankade.Model.Models;
using Darmankade.Model.ViewModels;
using Darmankade.ServiceBroker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Authentication
{
    public class AuthRepository : Repository<Auth>, IAuthRepository<Auth>
    {
        public AuthRepository(AuthDBContext dBContext) : base(dBContext)
        {
        }

        public bool AddAuth(UserViewModel userViewModel)
        {
            using var scope = new TransactionScope();
            var auth = new Auth()
            {
                Mobile = userViewModel.Mobile,
                Email = userViewModel.Email
            };
            RabbitMQHandler.Send("Darmankade", JsonConvert.SerializeObject(userViewModel));
            var result = Cretae(auth) != null;
            scope.Complete();
            return result;
        }
    }
}
