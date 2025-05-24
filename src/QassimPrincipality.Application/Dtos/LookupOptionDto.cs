using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos
{
    public class LookupOptionDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }

        public bool IsActive { get; set; }
        public LookupOptionType LookupOptionType { get; set; }
    }
}
