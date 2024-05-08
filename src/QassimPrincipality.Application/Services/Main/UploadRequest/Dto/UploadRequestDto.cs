using System.ComponentModel.DataAnnotations;
using Framework.Core.Globalization;
using Microsoft.AspNetCore.Http;
using QassimPrincipality.Application.Dtos;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestDto
    {
        public Guid? Id { get; set; }
        public string referralNumber { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]

        public string RequestName { get; set; }

        [Required(ErrorMessage = "يجب اختيار الصورة")]
        [FileExtensions(Extensions =".png,.jpg,.jpeg")]
        public IFormFile Photo { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "يجب اختيار ملف واحد على الاقل")]
        public List<IFormFile> OtherAttachments { get; set; }

        [MaxLength(14, ErrorMessage = "يجب ادخال 10 ارقام كحد اقصى")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يجب ادخال ارقام فقط")]
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        public string NafathNumber { get; set; }

        [MaxLength(14, ErrorMessage = "يجب ادخال 14 رقم كحد اقصى")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يجب ادخال ارقام فقط")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string PhoneNumber { get; set; }
        public int RequestTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? RequestDate { get; set; }
        public bool? IsApproved { get; set; }
        public string CreatedByFullName { get; set; }
        public string RejectReason { get; set; }
    }
}
