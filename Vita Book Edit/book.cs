using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Vita_Book_Edit
{
    [XmlRoot]
    public partial class book
    {
        public string descriptiontemplate = "![CDATA[id_description]] ";
        public string bookpath = null;
        public string title { get; set; }
        public string seriesName { get; set; }
        public string seriesOrdinal { get; set; }
        public string publisher { get; set; }
        public string description { get; set; }
        [XmlElement("authors")]
        public List<authors> authors { get; set; }
        [XmlElement("links")]
        public List<links> links { get; set; }

    }
}
