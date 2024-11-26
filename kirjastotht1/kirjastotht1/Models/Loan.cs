using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kirjastotht1.Models
{
    [Table("Loan")]
    public class Loan
    {
        [Key]
        [Column("LoanId")]
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? MemberId { get; set; }
        public DateTime LoanDate {  get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
