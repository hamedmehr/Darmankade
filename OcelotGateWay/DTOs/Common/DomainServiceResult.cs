using OcelotGateWay.Enums.Common;
using OcelotGateWay.Infrastructures.Extensions;

namespace OcelotGateWay.DTOs.Common
{
    public class DomainServiceResult<T>
    {
        public T Result { get; private set; } = default(T);
        public DomainStatusCode Status { get; private set; } = DomainStatusCode.Error;
        public string Message => Status.GetDescription();

        public bool IsSuccess => Status == DomainStatusCode.Success;

        public DomainServiceResult<T> SetResult(T result, DomainStatusCode status)
        {
            Result = result;
            Status = status;
            return this;
        }

        public DomainServiceResult<T> SetSuccessResult(T result)
        {
            Result = result;
            Status = DomainStatusCode.Success;
            return this;
        }

        public DomainServiceResult<T> SetErrorResult()
        {
            Result = default(T);
            Status = DomainStatusCode.Error;
            return this;
        }

        public DomainServiceResult<T> SetErrorResult(DomainStatusCode status)
        {
            if (status == DomainStatusCode.Success)
                status = DomainStatusCode.Error;
            Result = default(T);
            Status = status;
            return this;
        }
    }
}
