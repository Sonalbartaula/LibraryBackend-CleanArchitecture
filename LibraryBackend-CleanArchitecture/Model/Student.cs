using System.ComponentModel.DataAnnotations;

namespace LibraryBackend_CleanArchitecture.Model
{
    public enum MemberType
    {
        Student,
        Faculty,
        Staff
    }

    public enum Status
    {
        Active,
        Inactive,
        Suspended
    }
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public MemberType Type { get; set; } = MemberType.Student;

        public Status Status { get; set; } = Status.Active;

        public int BooksIssued { get; set; } = 0;

        public DateTime JoinedDate { get; set; } = DateTime.Now;

        public int overdueBooks { get; set; } = 0;

  


    }
}
