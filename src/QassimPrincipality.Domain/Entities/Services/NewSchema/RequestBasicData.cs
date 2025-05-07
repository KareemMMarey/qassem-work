using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Services.NewSchema
{
    public class RequestBasicData : LookupEntityBase
    {
        public Guid RequestId { get; set; }
        public string NationalId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool SourceFromNafath { get; set; }
        public bool SourceFromSpl { get; set; }
        public virtual ServiceRequest Request { get; set; }
    }
}
