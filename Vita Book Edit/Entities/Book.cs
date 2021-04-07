using System.Collections.Generic;
using System.Xml.Serialization;

namespace Vita_Book_Edit.Entities
{
    [XmlRoot("book")]
    public partial class Book
    {
        public string DescriptionTemplate = "![CDATA[id_description]] ";
        public string BookPath = null;
        public int Index { get; set; }
        public string DLCPath { get; set; }
        public int DLCIndex { get; set; }
        public string FilePath { get; set; }
        public bool FileExists { get; set; }
        public bool Selected { get; set; }
        [XmlElement("model")]
        public int Model { get; set; }
        [XmlElement("title")]
        public string Title { get; set; }
        [XmlElement("seriesName")]
        public string SeriesName { get; set; }
        [XmlElement("seriesOrdinal")]
        public string SeriesOrdinal { get; set; }
        [XmlElement("publisher")]
        public string Publisher { get; set; }
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("authors")]
        public List<Authors> Authors { get; set; }
        [XmlElement("links")]
        public List<Links> Links { get; set; }
    }
}
