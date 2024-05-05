using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.ShareData
{
    public class AddShareDataVM
    {
        public string UserFullName { get; set; }
        public string LegalJustificationDescription { get; set; } 
        [Required(ErrorMessage = "أدخل البريد الإلكتروني")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "أدخل  الجوال")]
        public string UserMobile { get; set; }
        [Required(ErrorMessage = "أدخل الغرض من طلب البيانات")]
        public string PurposeOfRequest { get; set; }
        [Required(ErrorMessage = "أدخل وصف البيانات المطلوبة")]
        public string Description { get; set; }
        public string IdentityNumber { get; set; }
        public int EntityId { get; set; }
        public bool IsApproved { get; set; }
        [BindProperty, Required(ErrorMessage = "لابد من تحديد هل توجد اتفاقية مشاركة البيانات")]
        public string IsShareAgreementExist { get; set; }
        [BindProperty,Required(ErrorMessage = "لابد من تحديد هل توجد بيانات شخصية")]
        public string IsContainsPersonalData { get; set; }
        [BindProperty,Required(ErrorMessage = "لابد من تحديد هل مقدم الطلب هو ممثل المكتب")]
        public string IsRequesterDataOfficePresenter { get; set; }
        [BindProperty,Required(ErrorMessage = "لابد من تحديد هل يوجد سند قانوني")]
        public string IsLegalJustification { get; set; }
        public bool IsAllowed { get; set; }
        
    }
}
