using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Main.Evaluation
{
    public class EvaluationDto
    {
        public Guid Id { get; set; }
        public int SubCategoryId { get; set; }
        public int EvalutionValue { get; set; }
        public bool? IsApproved { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
