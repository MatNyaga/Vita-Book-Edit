using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vita_Book_Edit
{
    public partial class book
    {
        public string descriptiontemplate = "![CDATA[id_description]] ";
        public string title { get; set; }
        public string seriesName { get; set; }
        public string seriesOrdinal { get; set; }
        public string publisher { get; set; }
        public string description { get; set; }
        public List<authors> author { get; set; }
        public List<links> link { get; set; }

    }
}
