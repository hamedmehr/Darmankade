using OcelotGateWay.DTOs.Common;
using OcelotGateWay.DTOs.RequestLogDTOs;

namespace OcelotGateWay.Services.Interfaces
{
    public interface IRequestLogService
    {
        DomainServiceResult<bool> SaveLog(SaveLogInputDTO inputDTO);
    }
}
