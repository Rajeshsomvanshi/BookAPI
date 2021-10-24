using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Management.DataModels.Entities
{
    public class Book
    {
        [Key]
        public int ISBN { get; set; }

        [Column("Name")]
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
