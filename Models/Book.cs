using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Models
{
    public class Book
    {
        public int BookID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Author ID must be greater than 0")]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1.00, 10000.00, ErrorMessage = "Price must be between 1.00 and 10000")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Book Price")]
        public decimal BookPrices { get; set; }

        public DateTime LastModified { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Available";

        [Required(ErrorMessage = "Author Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Author Name must be between 2 and 100 characters")]
        public string AuthorName { get; set; }

        // Navigation property - one book can have many borrowings
        //public virtual ICollection<Borrowing> Borrowings { get; set; }
    }
}