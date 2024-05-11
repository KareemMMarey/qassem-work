using Framework.Core.Data;
using PagedList.Core;
using QassimPrincipality.Application.Services.Main.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Main.Evaluation
{
    public class EvaluationSearchDto : PagingDto
    {
        public string CreatedBy { get; set; }
        public int SubCategoryId { get; set; }
        public int EvalutionValue { get; set; }
        public bool? IsApproved { get; set; }
        public int? TotalItemsCount { get; set; }
        public new StaticPagedList<EvaluationDto> Items { get; set; }
    }
}
