using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Web.Helpers;
using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.Request
{
    public class AddRequestViewModel: IValidatableObject
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public List<IFormFile> ListAttachments { get; set; }


        public string SerialNumber { get; set; }
        public string RequestTitle { get; set; }
        public int RequestSubClassificationId { get; set; }
        public int requestClassificationId { get; set; }
        public int RequestTypeId { get; set; }
        public int? ConsultantId { get; set; }
        public int LevelOfSecrecyId { get; set; }
        public string RequestSource { get; set; }
        public Guid? RequestOwnerId { get; set; }
        public AttachmentDto[] OpenSourceArFiles { get; set; }
        public AttachmentDto[] OpenSourceEnFiles { get; set; }
        public AttachmentDto[] CloseSourceArFiles { get; set; }
        public AttachmentDto[] CloseSourceEnFiles { get; set; }
        public AttachmentDto[] DataFiles { get; set; }
        public AttachmentDto[] SupportingFiles { get; set; }
        public DateTime? RequestDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var photo = ((AddRequestViewModel)validationContext.ObjectInstance).Photo;
            var extension = Path.GetExtension(photo.FileName);
            var size = photo.Length;

            if (!extension.ToLower().Equals(".jpg"))
                yield return new ValidationResult("File extension is not valid.");

            if (size > (5 * 1024 * 1024))
                yield return new ValidationResult("File size is bigger than 5MB.");
        }
    }
}
