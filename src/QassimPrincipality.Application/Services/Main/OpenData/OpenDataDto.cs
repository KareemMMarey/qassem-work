using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Main.OpenData
{
    public class OpenDataDto
    {
        public Guid Id { get; set; }
        [Display(Name ="اسم المستخدم")]
        public string UserFullName { get; set; }
        [Display(Name ="أسباب الرفض")]
        public string RejectReason { get; set; }
        [Display(Name = "البريد الالكتروني ")]

        public string UserEmail { get; set; }
        [Display(Name = "رقم الهوية")]
        public string IdentityNumber { get; set; }
        [Display(Name = "رقم الجوال")]
        public string UserMobile { get; set; }
        [Display(Name = "عنوان الشكوى")]
        public string Title { get; set; }
        [Display(Name = "التفاصيل")]
        public string Description { get; set; }
        [Display(Name = "نوع الطلب")]
        public int RequesterTypeId { get; set; }
        [Display(Name = "نوع الطلب")]
        public string RequesterTypeName{ get; set; }
        public string CreatedBy{ get; set; }
        public bool IsAllowed { get; set; }
        public bool? IsApproved { get; set; }
        public List<IFormFile> OtherAttachments { get; set; }
        [Display(Name = "وقت الطلب")]
        public DateTime CreatedOn { get; set; }
    }
}
