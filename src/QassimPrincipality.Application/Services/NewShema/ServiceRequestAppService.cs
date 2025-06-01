using Framework.Core.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;
using QassimPrincipality.Domain.Entities.Services.NewSchema;
using QassimPrincipality.Domain.Enums;
using QassimPrincipality.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.NewShema
{
    public class ServiceRequestAppService
    {
        private readonly IRepository<ServiceRequest> _serviceRequestRepository;
        private readonly IRepository<RequestAction> _requestActionRepository;
        private readonly IRepository<RequestBasicData> _requestBasicDataRepository;
        private readonly IRepository<RequestAdditionalData> _requestAdditionalDataRepository;
        private readonly IRepository<RequestAttachment> _requestAttachmentRepository;
        private readonly IRepository<EService> _serviceRepository;

        public ServiceRequestAppService(
            IRepository<ServiceRequest> serviceRequestRepository,
            IRepository<RequestBasicData> requestBasicDataRepository,
            IRepository<RequestAdditionalData> requestAdditionalDataRepository,
            IRepository<EService> serviceRepository,
            IRepository<RequestAction> requestActionRepository,
            IRepository<RequestAttachment> requestAttachmentRepository)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _requestBasicDataRepository = requestBasicDataRepository;
            _requestAdditionalDataRepository = requestAdditionalDataRepository;
            _requestAttachmentRepository = requestAttachmentRepository;
            _serviceRepository = serviceRepository;
            _requestActionRepository = requestActionRepository;
        }

        // Create a new service request (Draft)
        public async Task<ServiceRequestDto> CreateDraftRequestAsync(CreateServiceRequestDto dto)
        {
            var serviceRequest = dto.MapTo<ServiceRequest>();
            serviceRequest.Status = ServiceRequestStatus.Draft;
            serviceRequest.CreatedOn = DateTime.UtcNow;
            serviceRequest.UpdatedOn = DateTime.UtcNow;
            serviceRequest.RequestNumber = GenerateRequestNumber(dto.ServiceId);
            var savedRequest = await _serviceRequestRepository.InsertAsync(serviceRequest, true);
            return savedRequest.MapTo<ServiceRequestDto>();
        }

        // Save basic data for the first step
        public async Task<bool> SaveBasicDataAsync(Guid requestId, RequestBasicDataDto basicDataDto, string userId)
        {
            var basicData = basicDataDto.MapTo<RequestBasicData>();
            basicData.RequestId = requestId;
            basicData.CreatedBy = userId;
            basicData.CreatedOn = DateTime.UtcNow;
            basicData.UpdatedBy = userId;
            basicData.UpdatedOn = DateTime.UtcNow;
            basicData.IsActive = true;
            
            basicData.RequestDetails = basicDataDto.RequestDetails; 
            await _requestBasicDataRepository.InsertAsync(basicData, true);
            return true;
        }

        // Save additional data for subsequent steps
        public async Task<bool> SaveAdditionalDataAsync(Guid requestId, RequestAdditionalDataDto additionalDataDto, string userId)
        {
            var additionalData = additionalDataDto.MapTo<RequestAdditionalData>();
            additionalData.RequestId = requestId;
            additionalData.CreatedBy = userId;
            additionalData.CreatedOn = DateTime.UtcNow;
            additionalData.UpdatedBy = userId;
            additionalData.UpdatedOn = DateTime.UtcNow;
            additionalData.IsActive = true;

            await _requestAdditionalDataRepository.InsertAsync(additionalData, true);
            return true;
        }

        // Add attachment to a request
        public async Task<bool> AddAttachmentAsync(Guid requestId, RequestAttachmentDto attachmentDto, string userId)
        {
            var attachment = attachmentDto.MapTo<RequestAttachment>();
            attachment.RequestId = requestId;
            attachment.CreatedBy = userId;
            attachment.CreatedOn = DateTime.UtcNow;
            attachment.UpdatedBy = userId;
            attachment.UpdatedOn = DateTime.UtcNow;
            attachment.IsActive = false;  // Set to false until the request is submitted

            await _requestAttachmentRepository.InsertAsync(attachment, true);
            return true;
        }

        // Submit the service request
        public async Task<ServiceRequestDto> SubmitRequestAsync(Guid requestId, string userId)
        {
            var request = await _serviceRequestRepository.TableNoTracking
                .Include(r => r.Actions)
                .FirstOrDefaultAsync(r => r.Id == requestId);
            ValidateStatusTransition(request.Status, ServiceRequestStatus.Submitted);

            request.Status = ServiceRequestStatus.Submitted;
            request.UpdatedBy = userId;
            request.UpdatedOn = DateTime.UtcNow;

            // Activate all attachments
            var attachments = await _requestAttachmentRepository.Table
                .Where(a => a.RequestId == requestId)
                .ToListAsync();

            foreach (var attachment in attachments)
            {
                attachment.IsActive = true;
                attachment.IsValid = true;
                _requestAttachmentRepository.Update(attachment);
            }

            // Add action log
            request.Actions.Add(new RequestAction
            {
                NameEn = "Submitted",
                ActionDate = DateTime.UtcNow,
                ActionBy = userId,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
            });

            await _serviceRequestRepository.UpdateAsync(request, true);
            return request.MapTo<ServiceRequestDto>();
        }

        // Get service request by ID
        public async Task<ServiceRequestDto> GetRequestByIdAsync(Guid requestId)
        {
            var request = await _serviceRequestRepository.TableNoTracking
                .Include(r => r.BasicData)
                .Include(r => r.AdditionalData)
                .Include(r => r.Attachments)
                .Include(r => r.EService)
                .Include(r => r.Actions).FirstOrDefaultAsync(r => r.Id.ToString() == requestId.ToString());


            if (request == null)
                throw new KeyNotFoundException("Service request not found.");
            

            var model = request.MapTo<ServiceRequestDto>();

            if (request.Actions != null)
            {
                model.Actions = new List<RequestActionDto>();
                foreach (var action in request.Actions)
                {
                    model.Actions.Add(new RequestActionDto
                    {
                        ActionByUserId = action.ActionBy,
                        ActionDate = action.ActionDate,
                        ActionStatus = action.ToStatus,
                    });
                }

            }

            return model;
        }
        // Get service request by ID
        public async Task<ServiceRequestDto> GetIdAsync(Guid requestId)
        {
            var request = await _serviceRequestRepository.GetByIdAsync(requestId);

            if (request == null)
                throw new KeyNotFoundException("Service request not found.");

            return request.MapTo<ServiceRequestDto>();
        }

        // Change request status with action notes
        public async Task<bool> ChangeRequestStatusAsync(Guid requestId, ServiceRequestStatus newStatus, string userId, string actionNotes)
        {
            var request = await _serviceRequestRepository.GetByIdAsync(requestId);
            ValidateStatusTransition(request.Status, newStatus);

            request.Status = newStatus;
            request.UpdatedBy = userId;
            request.UpdatedOn = DateTime.UtcNow;

            if (request.Actions is null)
            {
                request.Actions = new List<RequestAction>();
            }


            request.Actions.Add(new RequestAction
            {
                FromStatus = request.Status,
                ToStatus = newStatus,
                NameEn = newStatus.ToString(),
                ActionDate = DateTime.UtcNow,
                ActionBy = userId,
                Notes = actionNotes,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow,
            });

            await _serviceRequestRepository.UpdateAsync(request, true);
            return true;
        }

        // Search requests with filters
        public async Task<List<ServiceRequestDto>> SearchRequestsAsync(RequestSearchFilterDto filter)
        {
            var query = _serviceRequestRepository.TableNoTracking
                //.Include(r => r.BasicData)
                //.Include(r => r.AdditionalData)
                //.Include(r => r.Attachments)
                //.Include(r => r.Actions)
                .Include(r => r.EService)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.UserId))
                query = query.Where(r => r.UserId == filter.UserId);

            if (!string.IsNullOrEmpty(filter.RequestNumber))
                query = query.Where(r => r.RequestNumber.Contains(filter.RequestNumber));

            if (filter.ServiceId.HasValue)
                query = query.Where(r => r.ServiceId == filter.ServiceId);

            if (filter.Status.HasValue)
                query = query.Where(r => r.Status == filter.Status);

            if (filter.StartDate.HasValue)
                query = query.Where(r => r.CreatedOn >= filter.StartDate);

            if (filter.EndDate.HasValue)
                query = query.Where(r => r.CreatedOn <= filter.EndDate);

            var results = await query.ToListAsync();
            return results.MapTo<List<ServiceRequestDto>>();
        }
        
        // Search requests with filters
        public async Task<List<SelectListDto>> GetServices()
        {
            var data = await _serviceRepository.TableNoTracking
                .Select(c=> new SelectListDto
                {
                    Id = c.Id,
                    NameAr = c.NameAr,
                    NameEn = c.NameEn
                })
                .ToListAsync();

            return data;
        }

        // Validate status transitions
        private void ValidateStatusTransition(ServiceRequestStatus currentStatus, ServiceRequestStatus newStatus)
        {
            if (currentStatus == ServiceRequestStatus.Approved || currentStatus == ServiceRequestStatus.Rejected)
                throw new InvalidOperationException("Cannot change status from a finalized state.");

            if (currentStatus == ServiceRequestStatus.Draft && newStatus != ServiceRequestStatus.Submitted)
                throw new InvalidOperationException("Draft can only transition to Submitted.");

            if (currentStatus == ServiceRequestStatus.Submitted && newStatus != ServiceRequestStatus.UnderReview)
                throw new InvalidOperationException("Submitted can only transition to UnderReview.");

            if (currentStatus == ServiceRequestStatus.UnderReview && (newStatus != ServiceRequestStatus.Approved && newStatus != ServiceRequestStatus.Rejected))
                throw new InvalidOperationException("UnderReview can only transition to Approved or Rejected.");
        }
       

        private string GenerateRequestNumber(int serviceId)
        {
            // Prefix
            string prefix = "QA_";

            // Get the current year as a 4-digit string (e.g., 2025)
            string year = DateTime.UtcNow.Year.ToString();

            // Format the Service ID (add leading zero if single digit)
            string servicePrefix = serviceId < 10 ? $"0{serviceId}" : serviceId.ToString().Substring(0, 2);

            // Generate a random 6-character alphanumeric code
            string uniqueCode = GenerateRandomAlphanumeric(6);

            // Combine prefix, year, service prefix, and unique code
            return $"{prefix}{year}{servicePrefix}{uniqueCode}";
        }

        private string GenerateRandomAlphanumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
