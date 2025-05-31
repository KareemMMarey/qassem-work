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
                _ => ""
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
