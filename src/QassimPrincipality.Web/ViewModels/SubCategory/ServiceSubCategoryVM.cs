using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.SubCategory
{
    public class ServiceSubCategoryVM
    {
        [Required(ErrorMessage = "أدخل اسم الخدمة")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "أدخل وصف الخدمة")]
        [MaxLength(400)]
        public string DescriptionAr { get; set; }
        [Required(ErrorMessage = "أدخل مدة الخدمة")]
        public string DurationDays { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "أدخل قيمة صحيحة")]
        public string ServiceRequierment { get; set; }

        [Required(ErrorMessage = "أدخل قيمة الخدمة")]
        [RegularExpression("([0-9]+)", ErrorMessage = "أدخل  قيمة صحيحة")]
        public decimal ServiceFees { get; set; }
        public string Audience { get; set; }
    }
}
