using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Application.Dtos
{
    public class ContactUsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MessageType { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

    }


}
