using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Main.ShareData
{
    public class ShareDataDto
    {
        public Guid Id { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string IdentityNumber { get; set; }
        public string RejectReason { get; set; }
        public string UserMobile { get; set; }
        public string Description { get; set; }
        public int EntityTypeId { get; set; }
        public string EntityName { get; set; }
        public string CreatedBy { get; set; }
        public string PurposeOfRequest { get; set; }
        public bool? IsShareAgreementExist { get; set; }
        public bool? IsContainsPersonalData { get; set; }
        public bool? IsRequesterDataOfficePresenter { get; set; }
        public bool? IsLegalJustification { get; set; }
        public bool IsAllowed { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LegalJustificationDescription { get; set; }

        [Display(Name = "رقم الطلب")]
        public string ReferralNumber { get; set; }
    }
}
