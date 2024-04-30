using AutoMapper;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType.Dto;
using QassimPrincipality.Application.Services.Lookups.Main.RequestClassification.Dto;
using QassimPrincipality.Domain.Entities.Lookups;
using QassimPrincipality.Domain.Entities.Lookups.Main;
using QassimPrincipality.Domain.Entities.Services.Main;
using Framework.Core.AutoMapper;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace QassimPrincipality.Application
{
    public class AppAutoMapperProfile : Profile, IMapperProfile
    {
        public AppAutoMapperProfile()
        {
            this.CreateMap<UserDto, ApplicationUser>().ReverseMap();
            this.CreateMap<ApplicationUserRoles, UserRolesDto>().ReverseMap();
            this.CreateMap<IdentityUserToken<Guid>, UserTokensDto>().ReverseMap();
            this.CreateMap<RequestClassificationDto, Classification>().ReverseMap();
            this.CreateMap<RequestTypeDto, RequestType>().ReverseMap();
            
            //this.CreateMap<OutputTypeDto, OutputType>().ReverseMap();
            this.CreateMap<ApplicationRole, RoleDto>().ReverseMap();
           
            this.CreateMap<UploadRequestDtoEdit, UploadRequest>()
                .ForMember(s => s.Attachments, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(s => s.ExistAttachments, opt => opt.MapFrom(s => s.Attachments));

            
            this.CreateMap<Attachment, AttachmentDto>().ReverseMap();
            

            this.CreateMap<UploadRequestDtoView, UploadRequest>()
                .ReverseMap();

            this.CreateMap<UploadRequestDtoAdd, UploadRequest>()
                .ReverseMap();

        }

        public int Order { get; set; }
    }
}
