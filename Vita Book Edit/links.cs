using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Vita_Book_Edit
{
#if AFAC
    public partial class link
    {
        [XmlAttribute("target")]
        public string target { get; }
    }

#endif

    [XmlRoot(ElementName = "link")]
    public class link
    {
        [XmlAttribute("target")]
        public string target;
        [XmlText]
        public string weblinkdescription { get; set; }
    }

    public partial class links
    {
        [XmlElement]
        public link link { get; set; }
        public string note { get; set; }
    }
}
