using System.ComponentModel.DataAnnotations;

namespace LibraryBackend_CleanArchitecture.Model
{
        public enum Bookstatus
        {
            Available,
            Unavailable
        }
        public class Book
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [StringLength(150)]
            public string Title { get; set; } = string.Empty;

            [Required]
            [StringLength(100)]
            public string Author { get; set; } = string.Empty;

            [Required]
            [StringLength(20)]
            public string ISBN { get; set; } = string.Empty;

            public string Categories { get; set; } = string.Empty;


            [Range(1, int.MaxValue, ErrorMessage = "Copies must be at least 1.")]
            public int TotalCopies { get; set; }


            [Range(0, int.MaxValue)]
            public int IssuedCopies { get; set; }

            public Bookstatus Status { get; set; } = Bookstatus.Available;


            public DateTime AddedDate { get; set; } = DateTime.Now;

        }
 }


