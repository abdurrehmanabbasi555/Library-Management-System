using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Models
{
    public class Member
    {
        public int MemberID
        {
            get; set;
        }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Display(Name = "Full Name")]
        public string Name
        {
            get; set;
        }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(150)]
        public string Email
        {
            get; set;
        }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone format")]
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get; set;
        }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(250)]
        public string Address
        {
            get; set;
        }

        [Display(Name = "Membership Date")]
        public DateTime MembershipDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Active";  // Active or Suspended
    }
}