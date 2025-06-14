using Microsoft.AspNetCore.Mvc.Localization;
using QassimPrincipality.Domain.Entities.Services.NewSchema;
using QassimPrincipality.Domain.Enums;

namespace QassimPrincipality.Web.Helpers.Business
{
    public static class WorkFlowHelper
    {
        public static string GetStatusClass(ServiceRequestStatus status)
        {
            return status switch
            {
                ServiceRequestStatus.Approved => "pc-green-status",
                ServiceRequestStatus.Submitted => "pc-blue-status",
                ServiceRequestStatus.UnderReview => "pc-blue-status",
                ServiceRequestStatus.Rejected => "pc-red-status",
                ServiceRequestStatus.RequiresCompletion => "pc-orange-status",
                ServiceRequestStatus.NotQualified => "pc-grey-status",
                _ => "",
            };
        }

        public static string GetStatusDisplay(ServiceRequestStatus status, IViewLocalizer localizer)
        {
            return status switch
            {
                ServiceRequestStatus.Approved => localizer["ApprovedStatus"].Value,
                ServiceRequestStatus.Submitted => localizer["SubmittedStatus"].Value,
                ServiceRequestStatus.Rejected => localizer["RejectedStatus"].Value,
                ServiceRequestStatus.RequiresCompletion => localizer[
                    "RequiresCompletionStatus"
                ].Value,
                ServiceRequestStatus.NotQualified => localizer["NotQualifiedStatus"].Value,
                ServiceRequestStatus.Cancelled => localizer[
                    ServiceRequestStatus.Cancelled.ToString()
                ].Value,
                ServiceRequestStatus.Draft => localizer[
                    ServiceRequestStatus.Draft.ToString()
                ].Value,
                ServiceRequestStatus.UnderReview => localizer[
                    ServiceRequestStatus.UnderReview.ToString()
                ].Value,
                _ => localizer["UnknownStatus"].Value,
            };
        }
        
        public static string GetLookupTypeDisplay(LookupOptionType status, IViewLocalizer localizer)
        {
            return status switch
            {
                LookupOptionType.Prison => localizer["Prison"].Value,
                LookupOptionType.ExitReasons => localizer["ExitReasons"].Value,
               
                _ => localizer["UnknownType"].Value,
            };
        }

        /// <summary>
        /// Returns a localized “action description” based on the new status.
        /// - If the status is Submitted, we say “تم إنشاء طلب جديد برقم {RequestNumber}”
        /// - Otherwise we say “تم تحديث الطلب إلى {LocalizedStatusText}”
        /// </summary>
        /// <param name="status">الحالة الجديدة</param>
        /// <param name="requestNumber">رقم الطلب (مثلاً “RR-12345”)</param>
        /// <param name="localizer">IViewLocalizer لقراءة جمل الـ .resx</param>
        public static string GetActionDescription(
            ServiceRequestStatus status,
            string requestNumber,
            IViewLocalizer localizer
        )
        {
            // 1) إذا كانت الحالة Submitted
            if (status == ServiceRequestStatus.Submitted)
            {
                // هذا المفتاح موجود لدينا في الـ .resx: "NewRequestCreated" => "تم إنشاء طلب جديد برقم {0}"
                // localizer["NewRequestCreated", requestNumber] سيُرجع جملة تحتوي على التأشير وقيمة requestNumber
                return localizer["NewRequestCreated", requestNumber].Value.Replace("{0}", requestNumber);
            }

            // 2) غير ذلك => “تم تحديث الطلب إلى {0}”
            // أولاً نحصل على النص المحلي للحالة (مثلاً "مقبول"، "قيد المراجعة"، ...)
            string localizedStatus = GetStatusDisplay(status, localizer);

            // ثم نستدعي المفتاح "RequestUpdatedTo" من الـ .resx
            // والذي في مثالي هو: "تم تحديث الطلب إلى {0}"
            var st =  localizer["RequestUpdatedTo", localizedStatus].Value;
            return st.Replace("{0}", localizedStatus);
        }

        public static string hideOption(ServiceRequestStatus status)
        {
            return status switch
            {
                ServiceRequestStatus.Approved => "hidden",
                ServiceRequestStatus.Submitted => "",
                ServiceRequestStatus.Rejected => "hidden",
                ServiceRequestStatus.RequiresCompletion => "",
                ServiceRequestStatus.NotQualified => "hidden",
                ServiceRequestStatus.Cancelled => "hidden",
                ServiceRequestStatus.Draft => "",
                ServiceRequestStatus.UnderReview => "",
                _ => "",
            };
        }

        public static string hideStatic(ServiceRequestStatus status)
        {
            return status switch
            {
                ServiceRequestStatus.Approved => "",
                ServiceRequestStatus.Submitted => "hidden",
                ServiceRequestStatus.Rejected => "",
                ServiceRequestStatus.RequiresCompletion => "hidden",
                ServiceRequestStatus.NotQualified => "",
                ServiceRequestStatus.Cancelled => "",
                ServiceRequestStatus.Draft => "hidden",
                ServiceRequestStatus.UnderReview => "hidden",
                _ => "",
            };
        }

        public static string Required(ServiceRequestStatus status)
        {
            return status switch
            {
                ServiceRequestStatus.Approved => "",
                ServiceRequestStatus.Submitted => "",
                ServiceRequestStatus.Rejected => "Required",
                ServiceRequestStatus.RequiresCompletion => "Required",
                ServiceRequestStatus.NotQualified => "",
                ServiceRequestStatus.Cancelled => "",
                ServiceRequestStatus.Draft => "",
                ServiceRequestStatus.UnderReview => "",
                _ => "",
            };
        }

        public static List<WorkFlowItem> items = new List<WorkFlowItem>
        {
            new WorkFlowItem
            {
                Id = ServiceRequestStatus.Draft,
                AllowedStatusses = new[] { ServiceRequestStatus.Submitted },
            },
            new WorkFlowItem
            {
                Id = ServiceRequestStatus.Submitted,
                AllowedStatusses = new[] { ServiceRequestStatus.UnderReview },
            },
            new WorkFlowItem
            {
                Id = ServiceRequestStatus.UnderReview,
                AllowedStatusses = new[]
                {
                    ServiceRequestStatus.Approved,
                    ServiceRequestStatus.Rejected,
                    ServiceRequestStatus.RequiresCompletion,
                },
            },
            new WorkFlowItem
            {
                Id = ServiceRequestStatus.RequiresCompletion,
                AllowedStatusses = new[]
                {
                    ServiceRequestStatus.Approved,
                    ServiceRequestStatus.Rejected,
                    ServiceRequestStatus.RequiresCompletion,
                },
            },
        };

        public static ServiceRequestStatus[] GetNextTransition(ServiceRequestStatus status)
        {
            return items.FirstOrDefault(c => c.Id == status)?.AllowedStatusses
                ?? Array.Empty<ServiceRequestStatus>();
        }
    }
}
