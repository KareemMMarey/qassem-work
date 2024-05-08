using System.ComponentModel.DataAnnotations;
using QassimPrincipality.Web.Helpers;

namespace QassimPrincipality.Web.ViewModels.Request
{
    public class AddRequestViewModel 
    {
        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(1 * 1024 * 1024)]
        [Required(ErrorMessage = "يجب اختيار الصورة")]
        [AllowedExtensions(
            new string[] { ".jpg", ".png", ".jpeg", ".pdf" },
            ErrorMessage = "يجب رفع الملفات بالصيغ ({1}) فقط"
        )]
        //[FileExtensions(Extensions = ".png,.jpg,.jpeg", ErrorMessage = "يجب رفع الملفات بالصيغ ({1}) فقط")]
        public IFormFile Photo { get; set; }

        [DataType(DataType.Upload)]
        [MaxFilesSize(10 * 1024 * 1024)]
        [AllowedExtensions(
            new string[] { ".jpg", ".png", ".jpeg", ".pdf" },
            ErrorMessage = "يجب رفع الملفات بالصيغ ({1}) فقط"
        )]
        //[FileExtensions(Extensions = ".png,.jpg,.jpeg,.pdf",ErrorMessage = "يجب رفع الملفات بالصيغ ({1}) فقط")]
        [MaxLength(10, ErrorMessage = "يجب ارفاق 10 ملفات فقط كحد اقصى")]
        [Required(ErrorMessage = "يجب اختيار ملف واحد على الاقل")]
        public List<IFormFile> ListAttachments { get; set; }

        [MaxLength(10, ErrorMessage = "يجب ادخال 10 ارقام كحد اقصى")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يجب ادخال ارقام فقط")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NafathNumber { get; set; } = null!;

        [MaxLength(14, ErrorMessage = "يجب ادخال 14 رقم كحد اقصى")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يجب ادخال ارقام فقط")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(50)]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string RequestName { get; set; } = null!;
        public int RequestTypeId { get; set; }


    }
}
