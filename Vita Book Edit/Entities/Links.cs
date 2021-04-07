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
    public class Link
    {
        [XmlAttribute("target")]
        public string Target { get; set; }
        [XmlText]
        public string WebLinkDescription { get; set; }
    }

    public partial class Links
    {
        [XmlElement("link")]
        public Link Link { get; set; }
        [XmlElement("note")]
        public string Note { get; set; }
    }
}
