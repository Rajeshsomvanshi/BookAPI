using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Management.DataModels.Entities
{
    public class Author
    {
        [Key]
        [Column("ID")]
        public int AuthorId { get; set; }
        [Column("Name")]
        public string AuthorName { get; set; }

    }
}
