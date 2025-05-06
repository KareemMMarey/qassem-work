using AutoMapper;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType.Dto;
using QassimPrincipality.Domain.Entities.Lookups;
using QassimPrincipality.Domain.Entities.Lookups.Main;
using QassimPrincipality.Domain.Entities.Services.Main;
using Framework.Core.AutoMapper;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using QassimPrincipality.Application.Services.Lookups.Main.EServiceCategory;
using QassimPrincipality.Application.Services.Lookups.Main.EServicesSubCategory;
using QassimPrincipality.Application.Services.Lookups.Main.CommonEService;
using QassimPrincipality.Application.Services.Main.Contact;
using QassimPrincipality.Application.Services.Main.ShareData;
using QassimPrincipality.Application.Services.Main.OpenData;
using QassimPrincipality.Application.Services.Main.Evaluation;
using Framework.Core.SharedServices.Entities;
using Framework.Core.SharedServices.Dto;
using QassimPrincipality.Application.Dtos.Content;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema.Content;

namespace QassimPrincipality.Application
{
    public class AppAutoMapperProfile : Profile, IMapperProfile
    {
        public AppAutoMapperProfile()
        {
            this.CreateMap<UserDto, ApplicationUser>().ReverseMap();
            this.CreateMap<RequestTypeDto, RequestType>().ReverseMap();
            this.CreateMap<UploadRequest, UploadRequestDtoAdd>().ReverseMap();
            this.CreateMap<EServiceCategory, EServiceCategoryDto>().ReverseMap();
            this.CreateMap<EServiceSubCategory, EServiceSubCategoryDto>().ReverseMap();
            this.CreateMap<EServiceSubCategory, CommonEServiceDto>().ReverseMap();
            this.CreateMap<EServiceCategory, CommonEServiceDto>().ReverseMap();
            this.CreateMap<UploadRequestDto, UploadRequest>().ReverseMap();
            this.CreateMap<ContactForm, ContactFormDto>().ReverseMap();
            this.CreateMap<ShareDataRequest, ShareDataDto>().ReverseMap();
            this.CreateMap<OpenDataRequest, OpenDataDto>().ReverseMap();
            this.CreateMap<ApplicationUser, UserDto>().ReverseMap();
            this.CreateMap<UserUpdateDto, UserDto>().ReverseMap();
            this.CreateMap<ApplicationRole, RoleDto>().ReverseMap();
            this.CreateMap<ApplicationUserRoles, UserRolesDto>().ReverseMap();
            this.CreateMap<IdentityUserToken<Guid>, UserTokensDto>().ReverseMap();
            this.CreateMap<AttachmentDto, Attachment>()
                .ForMember(c=>c.AttachmentType,s=>s.MapFrom(x=>x.AttachmentTypeId))
                .ReverseMap();
            this.CreateMap<ServiceEvaluation, EvaluationDto>().ReverseMap();
            this.CreateMap<LogDto, Log>().ReverseMap();

			CreateMap<News, NewsDto>();
			CreateMap<CreateNewsRequest, News>();

			CreateMap<Statistic, StatisticDto>();
			CreateMap<CreateStatisticRequest, Statistic>();

		}

        public int Order { get; set; }
    }
}
