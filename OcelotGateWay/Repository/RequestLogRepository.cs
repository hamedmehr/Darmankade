using OcelotGateWay.DTOs.Common;
using OcelotGateWay.DTOs.RequestLogDTOs;
using OcelotGateWay.Repository.Common;
using OcelotGateWay.Repository.Interfaces;

namespace OcelotGateWay.Repository
{
    public class RequestLogRepository : DapperRepository, IRequestLogRepository
    {
        public DomainServiceResult<bool> SaveLog(SaveLogInputDTO inputDTO)
        {
            string command = @"INSERT INTO dbo.RequestLogs
(
    [RequestDateTime],
    RemoteIpAddress,
    RemotePort,
    Method,
    AbsoluteUri,
    UserAgent,
    IMEI,
    [Authorization]
)
VALUES
(@RequestDateTime, @RemoteIpAddress, @RemotePort, @Method, @AbsoluteUri, @UserAgent, @IMEI, @Authorization);";

            return DapperExecuteCommand(command, inputDTO);
        }
    }
}
