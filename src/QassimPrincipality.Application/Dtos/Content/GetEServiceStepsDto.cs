using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos.Content
{
    public class GetEServiceStepsDto
    {

        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public bool HasApplicantStatus { get; set; } // صفة مقدم الطلب
        public bool HasTypeOfSummons { get; set; }
        public string ServiceCode { get; set; }
        public string IconUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        // steps
        public List<ServiceStepsDto> ServiceSteps { get; set; }
        public List<ServiceAttachmentDto> AttachmentTypes { get; set; }
    }
    public class ServiceStepsDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string StepNameAr { get; set; }
        public string StepNameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public int StepNumber { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
    }
    public class ServiceAttachmentDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public bool IsMandatory { get; set; }
        public int MaxSizeMB { get; set; }
        public string AllowedExtensions { get; set; }
    }

}
