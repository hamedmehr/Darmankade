namespace OcelotGateWay
{
    public class CheckAPITokenOutputDTO
    {
        public bool TokenIsValid { get; set; }
        public string UserID { get; set; }
        public string MobileNumber { get; set; }
        public string IMEI { get; set; }
    }
}
