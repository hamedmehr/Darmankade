using Microsoft.Extensions.Configuration;

namespace OcelotGateWay.Repository.Common
{
    public class ConnectionManager
    {
        private static string connectionStr = "";

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(connectionStr))
                {
                    connectionStr = ServiceProviderHandler.GetService<IConfiguration>()["ConnectionString"];
                }
                return connectionStr;
            }
        }
    }
}
