using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.OpenData
{
    public class AddOpenDataViewModel
    {
        

            public string UserFullName { get; set; }

            [Required(ErrorMessage = "أدخل البريد الإلكتروني")]
            public string UserEmail { get; set; }
            [Required(ErrorMessage = "أدخل  الجوال")]
            public string UserMobile { get; set; }
            [Required(ErrorMessage = "أدخل عنوان النموذج")]
            public string ContactTitle { get; set; }
            [Required(ErrorMessage = "أدخل نص الشكوى ")]
            public string Description { get; set; }
            public string IdentityNumber { get; set; }
            public int ContactTypeId { get; set; }
            public bool IsApproved { get; set; }


        
    }
}
