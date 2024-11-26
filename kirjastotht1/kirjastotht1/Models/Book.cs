using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kirjastotht1.Models

{
    [Table("Book")]
    public class Book
    {
        [Key]
        [Column("BookId")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int? PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
    }
}
