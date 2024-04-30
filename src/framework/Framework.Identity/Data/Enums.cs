namespace Framework.Identity.Data
{
    public enum Roles
    {
        Admin,//مدير النظام
        KnowledgeManagement,//
        VP,//
        Researcher,//
        KnowledgeManagementManager,//
        ResearchExcellenceDirector,//
        RequestOwner,//
        Employee,//
        ExternalRequestTeam,//
        InternalRequestTeam,//
        President,//
        RequestLead,//
        RADepartment,//
        SharedServices,
        Stakeholder,
        Communication,
        VPOfDataInformation,
        ChiefOfStaff,
        ResearchExcellenceHead,
        PresidentOffice,
        InstitutionExcellenceDirector,
        ResearchAndConsultation,
        ResearchVP,
        LegalVP,
        Data,
        AdvancedAnalytics,
        PublicEvaluation,
        Director,
        Manager,
        SecretStudies,
        TopSecretStudies,
        ExtremelySecretStudies,
        SharingNotPermittedStudies
    }

    public enum RolesCategory
    {
        All,
        KMS,
        Referral
    }

    public enum RoleType
    {
        Dept = 1,
        Subdept = 2,
        Director = 3,
        Manager = 4
    }
}