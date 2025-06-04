using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Identity.Data.Dtos
{
    public class ContactInfoDto
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Id { get; set; }
    }
}