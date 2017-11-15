using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHAT.UWP.Taiwan.PM25.Model
{
    public class PM25Model
    {
        public string Site { get; set; }
        public string county { get; set; }
        public string PM25 { get; set; }
        public string DataCreationDate { get; set; }
        public string ItemUnit { get; set; }

        public string PM25WithUnit { get { return $"{PM25} {ItemUnit}"; } }
    }
}
