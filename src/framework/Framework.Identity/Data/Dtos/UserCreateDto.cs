namespace Framework.Identity.Data.Dtos
{
    public class UserCreateDto : UserCreateOrUpdateDtoBase
    {
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public bool IsExternalUser { get; set; }
        public string EmailConfirmation { get; set; }
        public string EmployeeNumber { get; set; }
    }
}