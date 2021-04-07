using System.Xml.Serialization;

namespace Vita_Book_Edit.Entities
{    
    public partial class Authors
    {
        [XmlElement("author")] 
        public string Author { get; set; }
    }
}
