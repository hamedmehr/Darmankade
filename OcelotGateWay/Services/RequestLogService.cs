using OcelotGateWay.DTOs.Common;
using OcelotGateWay.DTOs.RequestLogDTOs;
using OcelotGateWay.Repository.Interfaces;
using OcelotGateWay.Services.Interfaces;

namespace OcelotGateWay.Services
{
    public class RequestLogService : IRequestLogService
    {
        #region Private Services

        IRequestLogRepository requestLogRepository;
        IRequestLogRepository RequestLogRepository
        {
            get
            {
                if (requestLogRepository == null)
                    requestLogRepository = ServiceProviderHandler.GetService<IRequestLogRepository>();
                return requestLogRepository;
            }
        }

        #endregion

        public DomainServiceResult<bool> SaveLog(SaveLogInputDTO inputDTO)
        {
            return RequestLogRepository.SaveLog(inputDTO);
        }
    }
}
