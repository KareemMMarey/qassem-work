using Framework.Core;
using Framework.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Framework.Identity.Data.Entities
{
    //test
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser(string userName, string fullName, string email = null, bool isActive = true)
        {
            Check.NotNull(userName, nameof(userName));

            Id = Guid.NewGuid().AsSequentialGuid();
            UserName = userName.ToLower();
            FullName = fullName;
            NormalizedUserName = userName.ToUpperInvariant();
            Email = email?.ToLower();
            NormalizedEmail = email?.ToUpperInvariant();
            SecurityStamp = Guid.NewGuid().ToString();
            IsActive = isActive;
        }

        public string FullName { get; set; }
        public string FullNameAr { get; set; }
        public string Department { get; set; }
        public string DepartmentAr { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string JobTitle { get; set; }
        public string JobTitleAr { get; set; }
        public string Bio { get; set; }
        public string Extension { get; set; }
        public string EmployeeNumber { get; set; }
        public Guid? ProfilePhotoId { get; set; }
		public string Nationality { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Neighborhood { get; set; }
		public string IdentityNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}