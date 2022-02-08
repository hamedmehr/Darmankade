using System;

namespace OcelotGateWay.DTOs.RequestLogDTOs
{
    public class SaveLogInputDTO
    {
        public DateTime RequestDateTime { get; set; }
        public string RemoteIpAddress { get; set; }
        public string RemotePort { get; set; }
        public string Method { get; set; }
        public string AbsoluteUri { get; set; }
        public string UserAgent { get; set; }
        public string IMEI { get; set; }
        public string Authorization { get; set; }
    }
}
