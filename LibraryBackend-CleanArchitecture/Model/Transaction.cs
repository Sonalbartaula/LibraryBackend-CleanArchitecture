using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend_CleanArchitecture.Model
{
    public enum TransactionStatus
    {
        Active,
        Overdue
    }
    public enum Issuestatus
    {
        Issued,
        Returned,
        Overdue
    }

    public class transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public string MemberName { get; set; }

        public string Isbn { get; set; }
        public string BookTitle { get; set; }

        public DateTime CheckoutDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(14);
        public DateTime? ReturnDate { get; set; }
        public Issuestatus IssueStatus { get; set; } = Issuestatus.Issued;

        public TransactionStatus Status { get; set; } = TransactionStatus.Active;

        public decimal Fine { get; set; } = 0;

        [NotMapped]
        public string ComputedStatus
        {
            get
            {
                if (ReturnDate.HasValue)
                    return "Returned";
                if (DateTime.Now > DueDate)
                    return "Overdue";
                return "Active";
            }
        }

        [NotMapped]
        public string TimeLeft
        {
            get
            {
                if (ReturnDate.HasValue)
                    return "Returned";
                var remaining = (DueDate - DateTime.Now).TotalDays;
                return remaining > 0
                    ? $"{Math.Ceiling(remaining)} days left"
                    : $"{Math.Abs(Math.Ceiling(remaining))} days overdue";
            }
        }
    }
}
