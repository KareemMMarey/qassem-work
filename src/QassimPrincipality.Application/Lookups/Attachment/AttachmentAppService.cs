using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Framework.Core;
using Framework.Core.Extensions;
using Framework.Core.SharedServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Domain.Entities.Lookups;
using QassimPrincipality.Domain.Interfaces;

namespace QassimPrincipality.Application.Lookups.Attachments
{
    public class AttachmentAppService
    {
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IRepository<AttachmentContent> _attachmentContentRepository;
        private readonly AppSettingsService _appSettingsService;

        public AttachmentAppService(
            IRepository<Attachment> attachmentRepository,
            AppSettingsService appSettingsService,
            IRepository<AttachmentContent> attachmentContentRepository
        )
        {
            _attachmentRepository = attachmentRepository;
            _appSettingsService = appSettingsService;
            _attachmentContentRepository = attachmentContentRepository;
        }

        public ReturnResult<Attachment> AddAttachment(
            IFormFile file,
            string title = null,
            string contentType = null,
            AttachmentDto attachment = null,
            string referralNumber = ""
        )
        {
            var result = new ReturnResult<Attachment>();
            var attResult = this.AddOrUpdateAttachment(
                file,
                null,
                title,
                contentType,
                attachment,
                referralNumber
            );
            if (!attResult.IsValid)
            {
                result.Merge(attResult);
                return result;
            }

            result.Value = attResult.Value;
            return result;
        }

        public ReturnResult<Attachment> AddOrUpdateAttachment(
            IFormFile file,
            Guid? attachmentId = null,
            string title = null,
            string contentType = null,
            AttachmentDto attachment = null,
            string referralNumber = ""
        )
        {
            var result = new ReturnResult<Attachment>();
            if (file == null)
            {
                result.AddErrorItem(string.Empty, "File Zero Length");
                return result;
            }

            if (file.Length <= 0)
            {
                result.AddErrorItem(string.Empty, "File Zero Length");
                return result;
            }

            if (
                !_appSettingsService.SaveFilesToDatabase
                && string.IsNullOrEmpty(_appSettingsService.AttachmentsPath)
            )
            {
                throw new Exception(
                    "File can not be saved. Current Settings is. SaveFileToDatabase=true and Attachment Path is Missing"
                );
            }

            //var fileBytes = new byte[file.Length];

            ////file.InputStream.Read(fileBytes, 0, file.Length);
            var ms = new MemoryStream();
            file.OpenReadStream().CopyTo(ms);

            result.Value = this.AddOrUpdateAttachment(
                file.FileName,
                contentType ?? file.ContentType,
                Convert.FromBase64String(attachment.FileContent),
                attachmentId,
                title,
                title,
                attachmentDto: attachment,
                referralNumber: referralNumber
            );
            return result;
        }

        public Attachment AddOrUpdateAttachment(
            string fileName,
            string contentType,
            byte[] fileBytes,
            Guid? attachmentId = null,
            string titleAr = null,
            string titleEn = null,
            string descriptionAr = null,
            string descriptionEn = null,
            int? itemOrder = null,
            AttachmentDto attachmentDto = null,
            string referralNumber = ""
        )
        {
            var isUpdateFile = attachmentId.HasValue && attachmentId.Value != Guid.Empty;

            var attachment = isUpdateFile
                ? _attachmentRepository.GetById(attachmentId.Value)
                : new Attachment { Id = Guid.NewGuid().AsSequentialGuid() };

            if (attachment == null)
            {
                throw new Exception(
                    "The Attachment File You are trying to update Does Not Exist in the database"
                );
            }

            //if (attachment.AttachmentContent == null)
            //{
            //    attachment.AttachmentContent = new AttachmentContent();
            //}

            //attachment.AttachmentContent.FileContent = fileBytes;

            attachment.TitleAr = titleAr;
            attachment.TitleEn = titleEn;
            attachment.DescriptionAr = descriptionAr ?? attachment.DescriptionAr;
            attachment.DescriptionEn = descriptionEn ?? attachment.DescriptionEn;
            attachment.ContentType = contentType;
            attachment.Extension = new FileInfo(fileName).Extension;
            attachment.FileName = string.IsNullOrEmpty(referralNumber)
                ? fileName
                : referralNumber + "_" + fileName;
            attachment.IsCloseSourceArabic = attachmentDto.IsCloseSourceArabic;
            attachment.IsCloseSourceEnglish = attachmentDto.IsCloseSourceEnglish;
            attachment.IsData = attachmentDto.IsData;
            attachment.IsSupporting = attachmentDto.IsSupporting;
            attachment.IsOpenSourceEnglish = attachmentDto.IsOpenSourceEnglish;
            attachment.IsOpenSourceArabic = attachmentDto.IsOpenSourceArabic;
            attachment.IsSanitizedDocument = attachmentDto.IsSanitizedDocument;

            // in updating delete old file
            if (isUpdateFile)
            {
                this.DeleteAttachmentFromFileSystem(attachment.FilePath);
            }

            attachment.FilePath = _appSettingsService.SaveFilesToDatabase
                ? null
                : this.SaveAttachmentToFileSystem(attachment, fileBytes);
            //attachment.AttachmentContent.Id = attachment.Id;
            //attachment.AttachmentContent.FileContent =
            //    _appSettingsService.SaveFilesToDatabase ? fileBytes : null;

            if (!isUpdateFile)
            {
                _attachmentRepository.Insert(attachment, true);

                if (_appSettingsService.SaveFilesToDatabase)
                {
                    AttachmentContent content = new AttachmentContent();
                    if (contentType.StartsWith("image/"))
                    {
                        if (contentType.Contains("svg"))
                        {
                            content.Thumbnail = fileBytes;
                        }
                        else
                        {
                            content.Thumbnail = this.GenerateThumbnail(fileBytes);
                        }
                    }
                    content.FileContent = fileBytes;
                    content.AttachmentId = attachment.Id;
                    _attachmentContentRepository.Insert(content, true);
                }
            }

            return attachment;
        }

        public Guid? UploadAttachmenAsync(AttachmentDto attachment, string referralNumber = "")
        {
            if (attachment == null)
            {
                return null;
            }
            //add new attachments
            var resultAttachment = AddAttachment(
                Base64ToImage(attachment),
                contentType: attachment.ContentType,
                attachment: attachment,
                referralNumber: referralNumber
            );

            return resultAttachment.Value.Id;
        }

        private IFormFile Base64ToImage(AttachmentDto attach)
        {
            byte[] bytes = Convert.FromBase64String(attach.FileContent);
            MemoryStream stream = new MemoryStream(bytes);
            var file = new FormFile(stream, 0, bytes.Length, attach.FileName, attach.FileName);
            return file;
        }

        private byte[] GenerateThumbnail(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                var thumb = new Bitmap(220, 220);
                using (var bmp = Image.FromStream(ms))
                {
                    using (var g = Graphics.FromImage(thumb))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.DrawImage(bmp, 0, 0, 220, 220);
                    }
                }

                using (var msWrite = new MemoryStream())
                {
                    thumb.Save(msWrite, ImageFormat.Png);
                    return msWrite.ToArray();
                }
            }
        }

        public void DeleteAttachmentFromFileSystem(string fileRelativePath)
        {
            if (string.IsNullOrEmpty(fileRelativePath))
            {
                return;
            }

            var filePath = $@"{_appSettingsService.AttachmentsPath}{fileRelativePath}";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string SaveAttachmentToFileSystem(Attachment attach, byte[] fileBytes)
        {
            var relativeFolderPath =
                $"\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}";
            var fullFolderPath = $"{_appSettingsService.AttachmentsPath}{relativeFolderPath}";
            var fileName = attach.Id + attach.Extension;
            var fileRelativePath = $@"{relativeFolderPath}\{fileName}";

            if (!Directory.Exists(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
            }
            using (
                var bw = new BinaryWriter(
                    File.Open(Path.Combine(fullFolderPath, fileName), FileMode.OpenOrCreate)
                )
            )
            {
                bw.Write(fileBytes);
            }
            return fileRelativePath;
        }

        public Attachment GetAttachmentForDownload(Guid? attachmentId)
        {
            var attachment = _attachmentRepository
                .TableNoTracking.Include(a => a.AttachmentContent)
                .Where(at => at.Id == attachmentId)
                .AsNoTracking()
                .SingleOrDefault();

            if (
                string.IsNullOrEmpty(_appSettingsService.AttachmentsPath)
                || string.IsNullOrEmpty(attachment?.FilePath)
            )
            {
                return attachment;
            }

            var filePath = $"{_appSettingsService.AttachmentsPath}{attachment.FilePath}";
            if (File.Exists(filePath))
            {
                attachment.AttachmentContent.FileContent = File.ReadAllBytes(filePath);
            }

            return attachment;
        }

        public void Remove(Guid id)
        {
            this._attachmentRepository.Delete(a => a.Id == id, true);
        }

        public void Update(Attachment attach)
        {
            this._attachmentRepository.Update(attach, true);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            return await this._attachmentRepository.DeleteAsync(a => a.Id == id, true);
        }

        public async Task<bool> RemoveSanitizedDocAsync(Guid requestId)
        {
            return await this._attachmentRepository.DeleteAsync(
                a => a.UploadRequestId == requestId && a.IsSanitizedDocument.Value,
                true
            );
        }

        public async Task<bool> RemoveRequestAttachmentsAsync(Guid requestId)
        {
            return await this._attachmentRepository.DeleteAsync(
                a => a.UploadRequestId == requestId,
                true
            );
        }
    }
}
