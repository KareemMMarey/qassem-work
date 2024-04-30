namespace Framework.Identity.Data.Dtos
{
    public abstract class UserCreateOrUpdateDtoBase
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string FullNameAr { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool LockoutEnabled { get; set; }

        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }
    }
}