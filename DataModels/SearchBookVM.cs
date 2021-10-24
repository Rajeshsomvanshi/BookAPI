using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Management.DataModels
{
    public class SearchBookVM
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
    }
}
