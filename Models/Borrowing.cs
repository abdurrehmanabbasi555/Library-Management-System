using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Models
{
    [Table("Borrowings")]
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Book ID is required")]
        [Display(Name = "Book ID")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Borrow date is required")]
        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }  // Nullable - null until book is returned

        [Required]
        [StringLength(50)]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Borrowed";

        // Navigation property (optional but useful)
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}