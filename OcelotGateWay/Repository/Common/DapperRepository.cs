using Dapper;
using OcelotGateWay.DTOs.Common;
using OcelotGateWay.Enums.Common;
using System.Data;
using System.Data.SqlClient;

namespace OcelotGateWay.Repository.Common
{
    public abstract class DapperRepository
    {
        public DomainServiceResult<bool> DapperExecuteCommand(string command, object inputParams = null)
        {
            var answer = new DomainServiceResult<bool>();

            try
            {
                using (IDbConnection db = new SqlConnection(ConnectionManager.ConnectionString))
                {
                    db.Execute(command, inputParams);

                    answer.SetSuccessResult(true);
                }
            }
            catch
            {
                answer.SetErrorResult(DomainStatusCode.DatabaseError);
            }

            return answer;
        }
    }
}
