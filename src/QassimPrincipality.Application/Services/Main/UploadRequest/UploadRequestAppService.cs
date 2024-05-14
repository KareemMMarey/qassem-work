using System.Linq.Expressions;
using AutoMapper.Internal;
using Framework.Core;
using Framework.Core.AutoMapper;
using Framework.Core.Extensions;
using Framework.Core.Notifications;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Lookups.Attachments;
using QassimPrincipality.Application.Lookups.Services;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using QassimPrincipality.Domain.Entities.Lookups;
using QassimPrincipality.Domain.Entities.Lookups.Main;
using QassimPrincipality.Domain.Entities.Services.Main;
using QassimPrincipality.Domain.Enums;
using QassimPrincipality.Domain.Interfaces;

namespace QassimPrincipality.Application.Services.Main.UploadRequest
{
    public class UploadRequestAppService
    {
        private readonly IRepository<Domain.Entities.Services.Main.UploadRequest> _uploadRequestRepository;
        private readonly IRepository<RequestType> _requestTypeRepository;
        private readonly IUserAppService _userAppService;
        private readonly AttachmentAppService _attachmentAppService;
        private readonly IRepository<Attachment> _attachmentRepository;

        private readonly AppSettingsService _appSettingsService;
        private readonly LookupAppService _lookupAppService;
        private readonly IRoleAppService _roleAppService;

        public UploadRequestAppService(
            IRepository<Domain.Entities.Services.Main.UploadRequest> uploadRequestRepository,
            IRepository<RequestType> requestTypeRepository,
            IUserAppService userAppService,
            AppSettingsService appSettingsService,
            AttachmentAppService attachmentAppService,
            LookupAppService lookupAppService,
            IRepository<Attachment> attachmentRepository,
            IRoleAppService roleAppService
        )
        {
            _uploadRequestRepository = uploadRequestRepository;

            _userAppService = userAppService;
            _appSettingsService = appSettingsService;
            _attachmentAppService = attachmentAppService;
            _lookupAppService = lookupAppService;
            _attachmentRepository = attachmentRepository;
            _roleAppService = roleAppService;
            _requestTypeRepository = requestTypeRepository;
        }

        public async Task<List<UploadRequestDto>> GetAllUploadRequests()
        {
            var uploadRequest = await _uploadRequestRepository.TableNoTracking.ToListAsync();
            return uploadRequest.MapTo<List<UploadRequestDto>>();
        }

        public async Task<UploadRequestDto> InsertAsync(UploadRequestDtoAdd UploadRequestDto)
        {
            var uploadRequest =
                UploadRequestDto.MapTo<Domain.Entities.Services.Main.UploadRequest>();
            //  uploadRequest.CreatedBy =  _userAppService.CurrentUser.Id.ToString();

            var user = await _userAppService.GetUserAsync(Guid.Parse(uploadRequest.CreatedBy));
            uploadRequest.CreatedByFullName = user.FullNameAr ?? user.FullName;

            uploadRequest.RequestNameAr = UploadRequestDto.RequestName;
            uploadRequest.RequestNameEn = UploadRequestDto.RequestName;

            uploadRequest = await _uploadRequestRepository.InsertAsync(uploadRequest, true);

            var photo = new AttachmentDto
            {
                FileName = UploadRequestDto.Photo.FileName, // photoPath.FirstOrDefault(),
                Size = UploadRequestDto.Photo.Length,
                UploadRequestId = uploadRequest.Id,
                FileContent = GetBase64String(UploadRequestDto.Photo),
                ContentType = UploadRequestDto.Photo.ContentType
            };
            SaveAttachment(
                new AttachmentDto[] { photo },
                uploadRequest.Id,
                UploadRequestDto.referralNumber,
                AttachmentTypes.PersonalPhoto
            );

            var others = UploadRequestDto.OtherAttachments.Select(c => new AttachmentDto
            {
                FileName = c.FileName, // photoPath.FirstOrDefault(),
                Size = c.Length,
                UploadRequestId = uploadRequest.Id,
                FileContent = GetBase64String(c),
                ContentType = c.ContentType
            });

            //attachments

            SaveAttachment(
                others.ToArray(),
                uploadRequest.Id,
                UploadRequestDto.referralNumber,
                AttachmentTypes.Others
            );

            //SaveAttachment(UploadRequestDto.OpenSourceEnFiles, uploadRequest.Id, UploadRequestDto.referralNumber);
            //SaveAttachment(UploadRequestDto.CloseSourceArFiles, uploadRequest.Id, UploadRequestDto.referralNumber);
            //SaveAttachment(UploadRequestDto.CloseSourceEnFiles, uploadRequest.Id, UploadRequestDto.referralNumber);
            //SaveAttachment(UploadRequestDto.DataFiles, uploadRequest.Id, UploadRequestDto.referralNumber);
            //SaveAttachment(UploadRequestDto.SupportingFiles, uploadRequest.Id, UploadRequestDto.referralNumber);


            if (uploadRequest.Id != Guid.Empty)
            {
                //if (!UploadRequestDto.ApplicantIsRequestOwner.Value)
                //{
                //    UploadRequestDto.RequestOwnerUserName = (
                //        await _userAppService.FindByIdAsync(UploadRequestDto.RequestOwnerId)
                //    ).UserName;
                //}
                //await CreateNewRequest(UploadRequestDto);
            }
            return uploadRequest.MapTo<UploadRequestDto>();
        }

        private string GetBase64String(IFormFile photo)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                photo.CopyTo(stream);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public async Task<UploadRequestDtoEdit> UpdateAsync(UploadRequestDtoEdit UploadRequestDto)
        {
            try
            {
                if (UploadRequestDto.Id == null)
                {
                    return null;
                }

                updateAttachments(UploadRequestDto);

                var uploadRequest =
                    UploadRequestDto.MapTo<Domain.Entities.Services.Main.UploadRequest>();
                uploadRequest = await _uploadRequestRepository.UpdateAsync(uploadRequest, true);
                UploadRequestDto.Id = uploadRequest.Id;

                string newRequestNumber = "";

                if (!string.IsNullOrEmpty(UploadRequestDto.SerialNumber))
                {
                    //await CompleteWorkflow(UploadRequestDto, newRequestNumber);
                }

                return uploadRequest.MapTo<UploadRequestDtoEdit>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AcceptOrReject(Guid id, bool isApproved, string notes = "")
        {
            try
            {
                var uploadRequest = await _uploadRequestRepository.GetByIdAsync(id);
                uploadRequest.IsApproved = isApproved;
                if (!isApproved)
                {
                    uploadRequest.RejectReason = notes;
                }
                uploadRequest = await _uploadRequestRepository.UpdateAsync(uploadRequest, true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void updateAttachments(
            UploadRequestDtoEdit UploadRequestDto,
            Guid? oldUploadRequestId = null
        ) //oldUploadRequestId will be fileed in change request
        {
            //attachments
            SaveAttachment(
                UploadRequestDto.OpenSourceArFiles,
                UploadRequestDto.Id.Value,
                UploadRequestDto.referralNumber
            );
            SaveAttachment(
                UploadRequestDto.OpenSourceEnFiles,
                UploadRequestDto.Id.Value,
                UploadRequestDto.referralNumber
            );
            SaveAttachment(
                UploadRequestDto.CloseSourceArFiles,
                UploadRequestDto.Id.Value,
                UploadRequestDto.referralNumber
            );
            SaveAttachment(
                UploadRequestDto.CloseSourceEnFiles,
                UploadRequestDto.Id.Value,
                UploadRequestDto.referralNumber
            );
            SaveAttachment(
                UploadRequestDto.DataFiles,
                UploadRequestDto.Id.Value,
                UploadRequestDto.referralNumber
            );
            SaveAttachment(
                UploadRequestDto.SupportingFiles,
                UploadRequestDto.Id.Value,
                UploadRequestDto.referralNumber
            );

            //remove attachments
            UploadRequestDto.DeletedAttachmentsIds.ForEach(id => _attachmentAppService.Remove(id));
        }

        public void SaveAttachment(
            AttachmentDto[] attachments,
            Guid? UploadRequestId,
            string referralNumber = "",
            AttachmentTypes attachmentType = AttachmentTypes.Others
        )
        {
            if (attachments == null)
                return;
            foreach (var attachment in attachments)
            {
                if (attachment == null)
                    return;
                attachment.UploadRequestId = UploadRequestId.Value;
                if (!string.IsNullOrEmpty(attachment.FileContent))
                {
                    _attachmentAppService.UploadAttachmenAsync(
                        attachment,
                        referralNumber,
                        attachmentType
                    );
                }
                else
                {
                    //var a = _attachmentRepository.TableNoTracking.FirstOrDefault(a => a.Id == attachment.Id);
                    //if (a.Id == attachment.Id) continue;
                    //var fileContent = _attachmentRepository.TableNoTracking.FirstOrDefault(a => a.Id == attachment.Id).FileContent;
                    //MemoryStream stream = new MemoryStream(fileContent);
                    //var file = new FormFile(stream, 0, fileContent.Length, attachment.FileName, attachment.FileName);
                    //_attachmentAppService.AddAttachment(file, contentType: attachment.ContentType, attachment: attachment);

                    var att = _attachmentRepository.TableNoTracking.FirstOrDefault(a =>
                        a.Id == attachment.Id
                    );
                    if (att.UploadRequestId == UploadRequestId.Value)
                    {
                        continue;
                    }
                    att.UploadRequestId = attachment.UploadRequestId.Value;
                    att.Id = Guid.NewGuid();

                    _attachmentRepository.Insert(att, true);
                }
            }
        }

        public Guid? SaveAttachment(AttachmentDto attachment, Guid? attachmentId)
        {
            if (attachment == null && !attachmentId.HasValue)
                return null;
            return attachment == null
                ? attachmentId.Value
                : _attachmentAppService.UploadAttachmenAsync(attachment);
        }

        public async Task<bool> UploadSanitizedDocumentAsync(
            SanitizedDocumentDto sanitizedDocumentDto
        )
        {
            await _attachmentAppService.RemoveSanitizedDocAsync(
                sanitizedDocumentDto.UploadRequestId.Value
            );
            if (
                sanitizedDocumentDto.SanitizedDocuments != null
                & sanitizedDocumentDto.SanitizedDocuments.Length > 0
            )
            {
                SaveAttachment(
                    sanitizedDocumentDto.SanitizedDocuments,
                    sanitizedDocumentDto.UploadRequestId.Value
                );
            }

            return false;
        }

        public async Task<SanitizedDocumentDto> GetSanitizedDocumentAsync(Guid UploadRequestId)
        {
            var uploadRequest = await _uploadRequestRepository
                .TableNoTracking.Include(s => s.Attachments)
                .FirstOrDefaultAsync(s => s.Id == UploadRequestId);
            if (
                uploadRequest.Attachments.Any(a =>
                    a.IsSanitizedDocument.HasValue && a.IsSanitizedDocument.Value
                )
            )
            {
                var sanitizedAttachments = uploadRequest
                    .Attachments.Where(a =>
                        a.IsSanitizedDocument.HasValue && a.IsSanitizedDocument.Value == true
                    )
                    .ToList();
                //sanitizedAttachments.ForEach(b => b.AttachmentContent.FileContent = null);
                return new SanitizedDocumentDto()
                {
                    SanitizedDocuments = sanitizedAttachments.MapTo<AttachmentDto[]>(),
                    LevelOfSecrecy = 1
                };
            }
            return new SanitizedDocumentDto() { LevelOfSecrecy = 1 };
        }

        public async Task<UploadRequestDto> GetById(Guid id)
        {
            try
            {
                var entity = await _uploadRequestRepository
                    .TableNoTracking.Include(c => c.RequestType)
                    .FirstOrDefaultAsync(c => c.Id == id);

                var attIds = await _attachmentRepository
                    .TableNoTracking.Where(c => c.UploadRequestId == id)
                    .Select(c => c.Id)
                    .ToArrayAsync();
                var atts = (await _attachmentAppService.GetAttachmentsForDownload(attIds)).MapTo<
                    List<AttachmentDto>
                >();

                var UploadRequestDto = entity.MapTo<UploadRequestDto>();
                UploadRequestDto.RequestTypeName = entity.RequestType.RequestTypeName;
                UploadRequestDto.PhotoId = atts.FirstOrDefault(c =>
                    c.AttachmentTypeId == (int)AttachmentTypes.PersonalPhoto
                ).Id;

                UploadRequestDto.OtherAttachmentIds = atts.Where(c =>
                        c.AttachmentTypeId == (int)AttachmentTypes.Others
                    )
                    .Select(c => c.Id)
                    .ToList();

                return await Task.FromResult(UploadRequestDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private IFormFile GetFile(Attachment attachment, AttachmentDto attach)
        {
            MemoryStream stream = new MemoryStream(attachment.AttachmentContent.FileContent);
            return new FormFile(
                stream,
                0,
                attachment.AttachmentContent.FileContent.Length,
                attach.FileName,
                attach.FileName
            );
        }

        public async Task<UploadRequestDtoView> GetByIdViewMode(Guid id)
        {
            var entity = await _uploadRequestRepository
                .TableNoTracking.Include(s => s.Attachments)
                .Include(s => s.RequestType)
                .FirstOrDefaultAsync();

            var UploadRequestDto = entity.MapTo<UploadRequestDtoView>();
            UploadRequestDto.Attachments.ForEach(b => b.FileContent = "");

            fillAccessAuthority(UploadRequestDto);
            if (!checkCurrentUserHasAccess(UploadRequestDto))
            {
                return UploadRequestDto;
            }

            return UploadRequestDto;
        }

        //
        private bool checkCurrentUserHasAccess(UploadRequestDto UploadRequestDto)
        {
            var currentUser = _userAppService.CurrentUser;
            var roles = _userAppService.CurrentUserRoles.ConvertAll(d => d.ToLower());
            return true;
        }

        public async Task<UploadRequestDtoEdit> GetByIdEditMode(Guid id)
        {
            try
            {
                var entity = await _uploadRequestRepository
                    .TableNoTracking.Include(s => s.Attachments)
                    .Include(s => s.RequestType)
                    .FirstOrDefaultAsync();
                var UploadRequestDto = entity.MapTo<UploadRequestDtoEdit>();
                UploadRequestDto.ExistAttachments.ForEach(b => b.FileContent = "");
                return UploadRequestDto;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var obj = await _uploadRequestRepository.TableNoTracking.FirstOrDefaultAsync(m =>
                m.Id == id
            );
            if (obj != null)
            {
                return await _uploadRequestRepository.DeleteAsync(m => m.Id == id, true);
            }
            else
            {
                return false;
            }
        }

        public async Task<UploadRequestSearchDto> SearchForHeaderAsync(UploadRequestSearchDto model)
        {
            var filters = new List<
                Expression<Func<Domain.Entities.Services.Main.UploadRequest, bool>>
            >
            {
                a =>
                    (
                        a
                            .ReferralNumber.ToLower()
                            .Trim()
                            .Contains(model.ReferralNumber.ToLower().Trim())
                    )
            };

            Func<
                IQueryable<Domain.Entities.Services.Main.UploadRequest>,
                IOrderedQueryable<Domain.Entities.Services.Main.UploadRequest>
            > orderBy;
            orderBy = a => a.OrderByDescending(b => b.CreatedOn);

            var result = _uploadRequestRepository.SearchAndSelectWithFilters(
                model.PageNumber,
                model.PageSize ?? _appSettingsService.DefaultPagerPageSize,
                orderBy,
                a => a.MapTo<UploadRequestDto>(),
                filters
            );

            model.Items = new StaticPagedList<UploadRequestDto>(
                result,
                result.PageNumber,
                result.PageSize,
                result.TotalItemCount
            );

            model.TotalItemsCount = model.Items.TotalItemCount;

            return await Task.FromResult(model);
        }

        private void fillAccessAuthority(UploadRequestDtoView model)
        {
            var currentUser = _userAppService.CurrentUser;
        }

        public async Task<UploadRequestSearchDto> SearchAsync(
            UploadRequestSearchDto UploadRequestSearchDto
        )
        {
            var filters =
                new List<Expression<Func<Domain.Entities.Services.Main.UploadRequest, bool>>>();

            if (UploadRequestSearchDto.IsApproved != null)
                filters.Add(a => a.IsApproved == UploadRequestSearchDto.IsApproved);

            if (UploadRequestSearchDto.isPending != null)
                filters.Add(a => a.IsApproved == null);

            if (!UploadRequestSearchDto.CreatedBy.IsNullOrWhiteSpace())
                filters.Add(a => a.CreatedBy == UploadRequestSearchDto.CreatedBy);

            Func<
                IQueryable<Domain.Entities.Services.Main.UploadRequest>,
                IOrderedQueryable<Domain.Entities.Services.Main.UploadRequest>
            > orderBy;
            orderBy = a => a.OrderByDescending(b => b.CreatedOn);

            Framework.Core.PagedList<UploadRequestDto> result;

            result = _uploadRequestRepository.SearchAndSelectWithFilters(
                UploadRequestSearchDto.PageNumber,
                UploadRequestSearchDto.PageSize ?? _appSettingsService.DefaultPagerPageSize,
                orderBy,
                a => a.MapTo<UploadRequestDto>(),
                filters
            );

            UploadRequestSearchDto.Items = new StaticPagedList<UploadRequestDto>(
                result,
                result.PageNumber,
                result.PageSize,
                result.TotalItemCount
            );

            UploadRequestSearchDto.TotalItemsCount = UploadRequestSearchDto.Items.TotalItemCount;
            return await Task.FromResult(UploadRequestSearchDto);
        }
    }
}
