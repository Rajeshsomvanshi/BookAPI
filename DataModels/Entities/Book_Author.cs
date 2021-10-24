using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Management.DataModels.Entities
{
    public class Book_Author
    {
        
        [Column("AuthorID")]
        public int Id { get; set; }
       

        [Column("BookID")]
        public int BookId { get; set; }
    }
}
