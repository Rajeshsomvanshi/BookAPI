using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Management.DataModels
{
    public class BookVM
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }

        public List<string> Authors { get; set; }
    }
}
