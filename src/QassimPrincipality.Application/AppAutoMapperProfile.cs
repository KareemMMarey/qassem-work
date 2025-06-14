using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Framework.Core.AutoMapper;
using Framework.Core.SharedServices.Dto;
using Framework.Core.SharedServices.Entities;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType.Dto;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Application.Services.Lookups.Main.EServicesSubCategory;
using QassimPrincipality.Application.Services.Lookups.Main.CommonEService;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Application.Services.Main.ShareData;
using QassimPrincipality.Application.Services.Main.OpenData;
using QassimPrincipality.Application.Services.Main.Evaluation;
using QassimPrincipality.Application.Dtos.Content;
using QassimPrincipality.Domain.Entities.Lookups;
using QassimPrincipality.Domain.Entities.Lookups.Main;
using QassimPrincipality.Domain.Entities.Services.Main;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema.Content;
using QassimPrincipality.Domain.Entities.Services.NewSchema;
using QassimPrincipality.Domain.Enums;

namespace QassimPrincipality.Application
{
    public class AppAutoMapperProfile : Profile, IMapperProfile
    {
        public AppAutoMapperProfile()
        {
            // User Mappings
            CreateMap<UserDto, ApplicationUser>().ReverseMap();
            CreateMap<UserUpdateDto, UserDto>().ReverseMap();
            CreateMap<ApplicationUserRoles, UserRolesDto>().ReverseMap();
            CreateMap<IdentityUserToken<Guid>, UserTokensDto>().ReverseMap();
            CreateMap<ApplicationRole, RoleDto>().ReverseMap();

            // Request Mappings
            CreateMap<RequestTypeDto, RequestType>().ReverseMap();
            CreateMap<UploadRequest, UploadRequestDtoAdd>().ReverseMap();
            CreateMap<UploadRequestDto, UploadRequest>().ReverseMap();
            CreateMap<AttachmentDto, Attachment>()
                .ForMember(c => c.AttachmentType, s => s.MapFrom(x => x.AttachmentTypeId))
                .ReverseMap();

            // Contact Mappings
            CreateMap<ContactForm, ContactFormDto>().ReverseMap();
            CreateMap<ShareDataRequest, ShareDataDto>().ReverseMap();
            CreateMap<OpenDataRequest, OpenDataDto>().ReverseMap();

            // Evaluation Mappings
            CreateMap<ServiceEvaluation, EvaluationDto>().ReverseMap();
            CreateMap<LogDto, Log>().ReverseMap();

            // EService Category Mappings
            CreateMap<EServiceCategory, EServiceCategoryDto>().ReverseMap();
            CreateMap<EServiceSubCategory, EServiceSubCategoryDto>().ReverseMap();
            CreateMap<EServiceSubCategory, CommonEServiceDto>().ReverseMap();
            CreateMap<EServiceCategory, CommonEServiceDto>().ReverseMap();

            //SharedContactForm
            CreateMap<ContactUsModel,SharedContactForm>()
                .ForMember(c=> c.FirstName,s=> s.MapFrom(x=> x.FirstName))
                .ForMember(c=> c.LastName,s=> s.MapFrom(x=> x.LastName))
                .ForMember(c=> c.MessageType, s=> s.MapFrom(x=> x.MessageType == ContactMessageType.Inquiry.ToString() ? ContactMessageType.Inquiry : ContactMessageType.Request))
                .ForMember(c=> c.Email, s=> s.MapFrom(x=> x.Email))
                .ForMember(c=> c.Subject, s=> s.MapFrom(x=> x.Subject))
                .ForMember(c=> c.Message, s=> s.MapFrom(x=> x.Message))
                .ReverseMap();

            // News Mappings
            CreateMap<News, NewsDto>()
                .ForMember(c=> c.PublishDateString,s=> s.MapFrom(x=> x.PublishDate.ToString("yyyy-MM-dd", new CultureInfo("en-US"))))

                .ReverseMap();
            CreateMap<CreateNewsRequest, News>().ReverseMap();

            // Statistic Mappings
            CreateMap<Statistic, StatisticDto>().ReverseMap();
            CreateMap<CreateStatisticRequest, Statistic>().ReverseMap();

            // Page Feedback Mappings
            CreateMap<PageFeedback, PageFeedbackDto>().ReverseMap();

            // EService Mappings
            CreateMap<EService, GetEServiceListHome>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.DescriptionAr, opt => opt.MapFrom(src => src.DescriptionAr))
                .ForMember(dest => dest.DescriptionEn, opt => opt.MapFrom(src => src.DescriptionEn))
                .ForMember(dest => dest.ServiceCode, opt => opt.MapFrom(src => src.ServiceCode))
                .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.IconUrl))
                .ForMember(dest => dest.HasApplicantStatus, opt => opt.MapFrom(src => src.HasApplicantStatus))
                .ForMember(dest => dest.HasTypeOfSummons, opt => opt.MapFrom(src => src.HasTypeOfSummons))
                .ForMember(dest => dest.CategoryNameAr, opt => opt.MapFrom(src => src.ServicesCategory != null ? src.ServicesCategory.NameAr : string.Empty))
                .ForMember(dest => dest.CategoryNameEn, opt => opt.MapFrom(src => src.ServicesCategory != null ? src.ServicesCategory.NameEn : string.Empty))
                .ForMember(dest => dest.AudienceTypeAr, opt => opt.MapFrom(src => src.EServiceDetails != null ? src.EServiceDetails.AudienceTypeAr : string.Empty))
                .ForMember(dest => dest.AudienceTypeEn, opt => opt.MapFrom(src => src.EServiceDetails != null ? src.EServiceDetails.AudienceTypeEn : string.Empty))
                .ForMember(dest => dest.ExecutionTimeAr, opt => opt.MapFrom(src => src.EServiceDetails != null ? src.EServiceDetails.ExecutionTimeAr : string.Empty))
                .ForMember(dest => dest.ExecutionTimeEn, opt => opt.MapFrom(src => src.EServiceDetails != null ? src.EServiceDetails.ExecutionTimeEn : string.Empty))
                .ForMember(dest => dest.CostAr, opt => opt.MapFrom(src => src.EServiceDetails != null ? src.EServiceDetails.CostAr : string.Empty))
                .ForMember(dest => dest.CostEn, opt => opt.MapFrom(src => src.EServiceDetails != null ? src.EServiceDetails.CostEn : string.Empty))

                .ForMember(dest => dest.RateValue, opt => opt.MapFrom(src =>
                    src.Ratings != null && src.Ratings.Any()
                    ? src.Ratings.Average(r => r.RatingValue).ToString("0.0")
                    : "0.0"))
                .ReverseMap();

            CreateMap<EService, GetEServiceDetailsDto>()
     // Basic Property Mappings
     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
     .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
     .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
     .ForMember(dest => dest.DescriptionAr, opt => opt.MapFrom(src => src.DescriptionAr))
     .ForMember(dest => dest.DescriptionEn, opt => opt.MapFrom(src => src.DescriptionEn))
     .ForMember(dest => dest.ServiceCode, opt => opt.MapFrom(src => src.ServiceCode))
     .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.IconUrl))
     .ForMember(dest => dest.ServiceController, opt => opt.MapFrom(src => src.ServiceController))
     .ForMember(dest => dest.ServiceActionMethos, opt => opt.MapFrom(src => src.ServiceActionMethos))
     .ForMember(dest => dest.HasApplicantStatus, opt => opt.MapFrom(src => src.HasApplicantStatus))
     .ForMember(dest => dest.HasTypeOfSummons, opt => opt.MapFrom(src => src.HasTypeOfSummons))

     // Category Name Mapping
     .ForMember(dest => dest.CategoryNameAr, opt => opt.MapFrom(src =>
         src.ServicesCategory != null ? src.ServicesCategory.NameAr : string.Empty))
     .ForMember(dest => dest.CategoryNameEn, opt => opt.MapFrom(src =>
         src.ServicesCategory != null ? src.ServicesCategory.NameEn : string.Empty))

     // EServiceDetails Mapping
     .ForMember(dest => dest.AudienceTypeAr, opt => opt.MapFrom(src =>
         src.EServiceDetails != null ? src.EServiceDetails.AudienceTypeAr : string.Empty))
     .ForMember(dest => dest.AudienceTypeEn, opt => opt.MapFrom(src =>
         src.EServiceDetails != null ? src.EServiceDetails.AudienceTypeEn : string.Empty))
     .ForMember(dest => dest.ExecutionTimeAr, opt => opt.MapFrom(src =>
         src.EServiceDetails != null ? src.EServiceDetails.ExecutionTimeAr : string.Empty))
     .ForMember(dest => dest.ExecutionTimeEn, opt => opt.MapFrom(src =>
         src.EServiceDetails != null ? src.EServiceDetails.ExecutionTimeEn : string.Empty))
     .ForMember(dest => dest.CostAr, opt => opt.MapFrom(src =>
         src.EServiceDetails != null ? src.EServiceDetails.CostAr : string.Empty))
     .ForMember(dest => dest.CostEn, opt => opt.MapFrom(src =>
         src.EServiceDetails != null ? src.EServiceDetails.CostEn : string.Empty))

     // Service Requirements Mapping (Based on Current Culture)
     .ForMember(dest => dest.ServiceRequirement, opt => opt.MapFrom(src =>
         src.EServiceRequirements != null
             ? src.EServiceRequirements
                 .Where(req => !req.IsPaper.Value)
                 .Select(req =>
                     CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar"
                         ? req.DescriptionAr
                         : req.DescriptionEn)
                 .ToList()
             : new List<string>()))

     // Service Flow Mapping (Based on Current Culture)
     .ForMember(dest => dest.ServiceFlow, opt => opt.MapFrom(src =>
         src.EServiceFlows != null
             ? src.EServiceFlows
                 .Select(flow =>
                     CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar"
                         ? flow.DescriptionAr
                         : flow.DescriptionEn)
                 .ToList()
             : new List<string>()))

     // Service FAQs Mapping (Based on Current Culture)
     .ForMember(dest => dest.ServiceFAQs, opt => opt.MapFrom(src =>
         src.FAQs != null
             ? src.FAQs
                 .Where(faq =>
                     (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? faq.NameAr : faq.NameEn) != null &&
                     (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? faq.AnswerAr : faq.AnswerEn) != null)
                 .Select(faq => new ServiceFAQDto
                 {
                     NameAr = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? faq.NameAr : null,
                     NameEn = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? null : faq.NameEn,
                     AnswerAr = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? faq.AnswerAr : null,
                     AnswerEn = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? null : faq.AnswerEn
                 })
                 .ToList()
             : new List<ServiceFAQDto>()))

     // Rating Mapping (Average Rating)
     .ForMember(dest => dest.RateValue, opt => opt.MapFrom(src =>
         src.Ratings != null && src.Ratings.Any()
             ? src.Ratings.Average(r => r.RatingValue).ToString("0.0")
             : "0.0"
     ));


            CreateMap<EService, GetEServiceStepsDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
            .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
            .ForMember(dest => dest.DescriptionAr, opt => opt.MapFrom(src => src.DescriptionAr))
            .ForMember(dest => dest.DescriptionEn, opt => opt.MapFrom(src => src.DescriptionEn))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.HasApplicantStatus, opt => opt.MapFrom(src => src.HasApplicantStatus))
            .ForMember(dest => dest.HasTypeOfSummons, opt => opt.MapFrom(src => src.HasTypeOfSummons))

            // Category Name Mapping
            .ForMember(dest => dest.CategoryNameAr, opt => opt.MapFrom(src => src.ServicesCategory != null ? src.ServicesCategory.NameAr : string.Empty))
            .ForMember(dest => dest.CategoryNameEn, opt => opt.MapFrom(src => src.ServicesCategory != null ? src.ServicesCategory.NameEn : string.Empty))
            .ForMember(dest => dest.ServiceSteps, opt => opt.MapFrom(src =>
             src.ServiceSteps != null? src.ServiceSteps
            .Select(step => new ServiceStepsDto
                {
                    StepNameAr = step.StepNameAr,
                    StepNameEn = step.StepNameEn,
                    NameAr = step.NameAr,
                    NameEn = step.NameEn,
                    Id = step.Id,
                    StepNumber = step.StepNumber,
                    IsRequired = step.IsRequired,
                    Order = step.Order
                }).ToList(): new List<ServiceStepsDto>()))
            .ForMember(dest => dest.AttachmentTypes, opt => opt.MapFrom(src =>
             src.AttachmentTypes != null? src.AttachmentTypes
            .Select(attachment => new ServiceAttachmentDto
                {

                    NameAr = attachment.NameAr,
                    NameEn = attachment.NameEn,
                    Id = attachment.Id,
                    IsMandatory = attachment.IsMandatory,
                    MaxSizeMB = attachment.MaxSizeMB,
                    DescriptionAr = attachment.DescriptionAr,
                    DescriptionEn = attachment.DescriptionEn,
                    AllowedExtensions = attachment.AllowedExtensions
                })
            .ToList(): new List<ServiceAttachmentDto>()));


            CreateMap<EService, GetEServiceAttachmentDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
            .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
            .ForMember(dest => dest.DescriptionAr, opt => opt.MapFrom(src => src.DescriptionAr))
            .ForMember(dest => dest.DescriptionEn, opt => opt.MapFrom(src => src.DescriptionEn))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.HasApplicantStatus, opt => opt.MapFrom(src => src.HasApplicantStatus))
            .ForMember(dest => dest.HasTypeOfSummons, opt => opt.MapFrom(src => src.HasTypeOfSummons))

            // Category Name Mapping
            .ForMember(dest => dest.CategoryNameAr, opt => opt.MapFrom(src => src.ServicesCategory != null ? src.ServicesCategory.NameAr : string.Empty))
            .ForMember(dest => dest.CategoryNameEn, opt => opt.MapFrom(src => src.ServicesCategory != null ? src.ServicesCategory.NameEn : string.Empty));



            
            //CreateMap<RequestBasicDataDto, RequestBasicData>();
            CreateMap<CreateServiceRequestDto, ServiceRequest>();
            // Map ServiceRequest to ServiceRequestDto
            CreateMap<ServiceRequest, ServiceRequestDto>()
                .ForMember(dest => dest.BasicData, opt => opt.MapFrom(src => src.BasicData))
                .ForMember(dest => dest.Actions, opt => opt.MapFrom(src => src.Actions))
                .ForMember(dest => dest.AdditionalData, opt => opt.MapFrom(src => src.AdditionalData))
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments))
                .ForMember(dest => dest.ServiceNameAr, opt => opt.MapFrom(src => src.EService != null ? src.EService.NameAr : string.Empty))
                .ForMember(dest => dest.ServiceNameEn, opt => opt.MapFrom(src => src.EService != null ? src.EService.NameEn : string.Empty))
                .ReverseMap();

            // Map ServiceRequestBasicData to ServiceRequestBasicDataDto
            CreateMap<RequestBasicData, RequestBasicDataDto>().ReverseMap();
            CreateMap<RequestAction, RequestActionDto>().ReverseMap();
            CreateMap<RequestAdditionalData, RequestAdditionalDataDto>().ReverseMap();
            CreateMap<RequestAttachmentDto, RequestAttachment>().ReverseMap();
            CreateMap<LookupOption, LookupOptionDto>().ReverseMap();
            CreateMap<Country, CountryDto>();
        }
       

        public int Order { get; set; }
    }
}
