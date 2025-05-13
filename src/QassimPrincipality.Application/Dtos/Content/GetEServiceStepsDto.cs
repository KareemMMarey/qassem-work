using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos.Content
{
    public class GetEServiceStepsDto
    {

        public int Id { get; set; }
        // steps
        public List<ServiceStepsDto> ServiceSteps { get; set; }
    }
    public class ServiceStepsDto
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string StepNameAr { get; set; }
        public string StepNameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public int StepNumber { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
    }

}
