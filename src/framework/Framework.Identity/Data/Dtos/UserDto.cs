using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Framework.Identity.Data.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string FullNameAr { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string DepartmentAr { get; set; }
        public string JobTitle { get; set; }
        public string JobTitleAr { get; set; }
        public string UserPrincipalName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        public List<SelectListItem> ExistRoles { get; set; }
        public string EmployeeNumber { get; set; }
        public string Bio { get; set; }
        public string Extension { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? ProfilePhotoId { get; set; }
        public string[] Skills { get; set; }
        public string[] Achievements { get; set; }
        public string[] Education { get; set; }
        public string[] Certificates { get; set; }
    }
}