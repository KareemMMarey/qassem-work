using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos
{
    public class CustomSelectListItem: SelectListItem
    {
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
    }
}
