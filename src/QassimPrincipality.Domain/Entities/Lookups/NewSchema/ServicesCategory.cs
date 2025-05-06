using Framework.Core.Data;
using Framework.Core.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZXing.QrCode.Internal.Mode;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
    public class ServicesCategory : LookupEntityBase
    {

        public List<EService> EServices { get; set; } = new List<EService>();


    }
}
