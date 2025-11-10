
namespace LibraryBackend_CleanArchitecture.Model.Dashboard
{
    public enum ActivityType
    {
        BookAdded,
        BookIssued,
        BookReturned,
        NewMember,
        BookRenew
    }

    public class Activity
    {
        public int Id { get; set; } // Primary key

        public ActivityType Type { get; set; } // Type of activity

        public string Title { get; set; } = string.Empty; // Book name or member name

        public string Subtitle { get; set; } = string.Empty; // Author + Librarian, Student + Semester, etc.

        public DateTime Date { get; set; } = DateTime.UtcNow; // Timestamp for "x hours ago"
    }
}
