namespace QassimPrincipality.Domain.Enums
{
    #region Workflow

    public enum RequestStatusEnum
    {
        Draft = 1,
        Submitted = 2,
        Rejected = 3,
        Approved = 4,
        ReEdit = 5,
    }

    public enum ProcessEnum
    {
        //KMS
        UploadRequestWF = 1,

        RequestAccessWF = 2,
        ChangeRequestWF = 3,
        DiscussionRequestWF = 4,

        //Referral
        ReferralRequestWF = 5,

        Ref_ResearchWF = 6,
        Ref_CompleteNeededInfoWF = 7,
        Ref_ExtensionRequestWF = 8,
        Ref_ExportLetterWF = 9
    }

    public enum ProcessActivityEnum
    {
        UploadRequestWF_RequestOwnerReview = 1,
        RequetsAccessWF_DirectorReview = 8,
        DiscussionRequestWF_Viewd = 15,
        ReferralRequestWF_PresidentOfficeReview = 17
    }

    public enum ReferralProcessActivityEnum
    {
        Ref_ResearchWF_ResExcHeadReview = 21,
        ReferralRequestWF_PresidentOfficeReview = 17
    }

    public enum ReferralRequestWFActions
    {
        RequestSent = 27,
        RequestSubmitted = 43,
        PresidentOfficeApprove = 28,
        NeededInfoApprove = 38,
        ResearchWF_ResExcHeadReviewApprove = 31,
        ResearchWF_ResExcHeadReviewNotComply = 32,
        ResearchWF_ResExcHeadReviewFurtherServices = 33,
        ResearchWF_ResExcHeadReviewExtensionReq = 35,
        Ref_ExtensionRequestWF_PresidentOfficeReviewApprove = 43,
        Ref_ExtensionRequestWF_EnterSiteExportNumberSubmit = 45,
        ChiefOfStaffReviewApprove = 46,
        Ref_CompleteRequiredWF_InstExcellenceNormalReferralType = 67,
        Ref_CompleteRequiredWF_PMOProject = 68,
        AnnouncementWF_LegalComply = 71,
        AnnouncementWF_LegalConsultation = 72,
        AnnouncementWF_LegalNotComply = 73,
        AnnouncementWF_LegalPublished = 74,
        AnnouncementWF_LegalUnpublished = 75,
        AnnouncementWF_LegalReviewOutputApprove = 80,
        DataInformationWF_DataInfoReviewSubmit = 82,
        DataInformationWF_DataInfoReviewNotComply = 83,
        DataInformationWF_DataInfoReviewExtensionReq = 84,
        DataInformationWF_DataInfoRevOutputReturn = 87,
        RepresentationWF_StakeholderManagerSubmit = 88,
        RepresentationWF_PresidentOfficeReviewApprove = 98,
        RepresentationWF_SharedServicesSiteExportNumSubmit = 112,
        RepresentationWF_ChiefStaffReviewSelect = 122,
        RepresentationWF_ResearchVpApprove = 123,
        RepresentationWF_ResearchVpSelect = 125,
        RepresentationWF_RequiredDeptApprove = 126,
        RepresentationWF_RequiredDeptSelect = 128,
        RepresentationWF_RequiredSubDeptApprove = 129
    }

    public enum ProcessActivityWFStatusEnum
    {
        UploadRequestWF_Request_Submitted = 9,
        UploadRequestWF_UnderReviewRequestOwner = 3,

        RequetsAccessWF_Request_Submitted = 15,
        DiscussionRequestWF_Pending = 22,
        UnderReviewPresidentOffice = 24
    }

    public enum UploadRequestWFActions
    {
        RequestSent = 13,
        ResearcherReEditResend = 12,
        RequestOwnerReEditResend = 11,
        DirectorApprove = 9,
    }

    public enum RequestAccessWFActions
    {
        RequestSubmitted = 24,
        VPApproved = 16,
        UnderReviewDirector = 10,
        DirectorReviewReject = 15
    }

    public enum DiscussionRequestWFActions
    {
        RequestSubmitted = 25
    }

    public enum ChangeRequestWFActions
    {
        ChangeRequestWF_RequestSent = 19,
        ChangeRequestWF_RequestOwnerSendChanges = 20,
        ChangeRequestWF_KMManagerApprove = 21,
        ChangeRequestWF_KMManagerReject = 22,
        ChangeRequestWF_KMManagerClose = 23
    }

    public enum ChangeRequestWFStatusEnum
    {
        Request_Submitted = 16,
        UnderChangeRequestOwner = 17,
        Approved = 19,
        Closed = 21
    }

    public enum ChangeRequestWFActivities
    {
        ChangeRequestWF_RequestOwnerProvideInfo = 12
    }

    #endregion Workflow

    #region App


    public enum ReferralTypesEnum
    {
        Research = 1,
        Representation = 2,
        DataAndInformation = 3,
        Announcement = 4,
        CompleteRequired = 5
    }

    public enum ReferralRolesEnum
    {
        SharedServices,
        Stakeholder,
        ReferralsManagement,
        Admin
    }

	#endregion App

	public enum ServiceRequestStatus
	{
		Draft = 0,
		Submitted = 1,
		UnderReview = 2,
		Approved = 3,
		Rejected = 4,
		Cancelled = 5,
        RequiresCompletion = 6,
        NotQualified = 7,   
    }
	public enum ServiceRequesterRelation
	{
		MySelf = 1,
		Delegated = 2,
		Lawyer = 3,
		FristClassRelative = 4
	}
    public enum LookupOptionType {
		Prison = 1,
	}
	public enum ContactMessageType
	{
		Request = 1,   // طلب
		Inquiry = 2    // تواصل
	}
	public enum TabType
	{
		Requirements = 1,   // طلب
		FAQ = 2    // تواصل
	}
    public enum AboutSectionType {
		About = 1,   
		Tasks = 2,
		Goals = 3,
		Policies = 4,
		Population = 5,
		General =6,
	}
}