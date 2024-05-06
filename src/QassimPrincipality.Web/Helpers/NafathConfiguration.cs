namespace QassimPrincipality.Web.Helpers
{
    public class NafathConfiguration
    {
        public string ApiUrl { get; set; }
        public string LocaApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApplicationKey { get; set; }
        public string NafathResponseChecker { get; set; }
        public string NafathCheckerApiKey { get; set; }
        public NafathBody NafathBody { get; set; }
        public NafathCheckRequstBody NafathCheckRequstBody { get; set; }

    }
    public class NafathCheckRequstBody
    {
        public string Action { get; set; }
        public NafathCheckRequstParameter Parameters { get; set; }
    }
    public class NafathCheckRequstParameter
    {
        public string transId { get; set; }
        public long id { get; set; }
        public long random { get; set; }
    }
    public class NafathBody
    {
        public string Action { get; set; }
        public NafathParameter Parameters { get; set; }
    }
    public class NafathParameter
    {
        public string service { get; set; }
        public long id { get; set; }
    }
    public class NafathResult
    {
        public string service { get; set; }
        public long id { get; set; }
    }
    public class ApiHeaders
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public enum NafathStatus
    {
        WAITING = 0, EXPIRED = 1, REJECTED = 2, COMPLETED = 3
    }
}
