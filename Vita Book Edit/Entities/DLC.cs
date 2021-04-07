using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vita_Book_Edit.Entities
{
    public class DLC
    {
        public int Index { get; set; }
        public string RootPath { get; set; }
        public List<Book> Books { get; set; }
        public string DLCName { get; set; }
    }
}
