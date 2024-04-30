namespace QassimPrincipality.Application.Dtos
{
    public class UserVM
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool LoginStatus { get; set; } = false;
        public List<string> userRoles { get; set; }

        public string Token { get; set; }
    }
}