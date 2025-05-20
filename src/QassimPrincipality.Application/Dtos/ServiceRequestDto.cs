using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace QassimPrincipality.Application.Dtos
{
    
    public class CreateServiceRequestDto
    {
        public Guid Id { get; set; }
        public int ServiceId { get; set; }
        public string UserId { get; set; }
        public string RequestNumber { get; set; } = string.Empty;   
        public ServiceRequesterRelation ServiceRequesterRelation { get; set; }
        //public RequestBasicDataDto BasicData { get; set; }
        //public RequestAdditionalDataDto AdditionalData { get; set; }
        //public List<AttachmentDto> Attachments { get; set; }
    }

    public class UpdateServiceRequestDto
    {
        public Guid Id { get; set; }
        public int ServiceId { get; set; }
        public string UserId { get; set; }
        public ServiceRequesterRelation ServiceRequesterRelation { get; set; }
        public RequestBasicDataDto BasicData { get; set; }
        public RequestAdditionalDataDto AdditionalData { get; set; }
    }

    

    public class ActionDto
    {
        public string ActionName { get; set; }
        public DateTime ActionDate { get; set; }
        public string PerformedBy { get; set; }
    }

    public class ServiceRequestDto
    {
        public Guid Id { get; set; }
        public int ServiceId { get; set; }
        public string UserId { get; set; }
        public string RequestNumber { get; set; } = string.Empty;
        public ServiceRequestStatus Status { get; set; }
        public ServiceRequesterRelation ServiceRequesterRelation { get; set; }
        public RequestBasicDataDto BasicData { get; set; }
        public RequestAdditionalDataDto AdditionalData { get; set; }
        public List<RequestAttachmentDto> Attachments { get; set; }
        public List<RequestActionDto> Actions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }



public class RequestBasicDataDto
    {
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("nationality")]
        public string Nationality { get; set; }

        [JsonPropertyName("idNumber")]
        public string IdNumber { get; set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("district")]
        public string District { get; set; }

        [JsonPropertyName("requestDetails")]
        public string RequestDetails { get; set; }
    }

    public class RequestAdditionalDataDto
    {
        public string NationalId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int? CountryId { get; set; }
        public int? PrisonFromId { get; set; }
        public int? PrisonToId { get; set; }
        public int? OtherDDLId { get; set; }
        public string RequestDetails { get; set; }
    }

    public class RequestAttachmentDto
    {
        public int AttachmentTypeId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public bool IsValid { get; set; }
    }

    public class RequestActionDto
    {
        public Guid ServiceRequestId { get; set; }
        public string ActionByUserId { get; set; }
        public string ActionType { get; set; }
        public string ActionDescription { get; set; }
        public DateTime ActionDate { get; set; }
    }

}
