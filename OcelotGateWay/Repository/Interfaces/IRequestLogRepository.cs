using OcelotGateWay.DTOs.Common;
using OcelotGateWay.DTOs.RequestLogDTOs;

namespace OcelotGateWay.Repository.Interfaces
{
    public interface IRequestLogRepository
    {
        DomainServiceResult<bool> SaveLog(SaveLogInputDTO inputDTO);
    }
}
