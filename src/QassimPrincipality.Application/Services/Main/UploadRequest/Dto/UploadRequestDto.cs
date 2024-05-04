using QassimPrincipality.Application.Dtos;
using Framework.Core.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestDto
    {
        public Guid? Id { get; set; }
        public string referralNumber { get; set; }
        public string RequestName { get; set; }
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        public List<IFormFile> OtherAttachments { get; set; }

        public string NafathNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int RequestTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? RequestDate { get; set; }
        public bool? IsApproved { get; set; }
        public string CreatedByFullName { get; set; }
        public string RejectReason { get; set; }

    }
}