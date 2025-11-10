using LibraryBackend_CleanArchitecture.Model;

namespace LibraryBackend_CleanArchitecture.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync(int pageNumber, int pageSize);
        Task<Student?> GetStudentByIdAsync(int id);
        Task<IEnumerable<Student>> SearchStudentsAsync(string searchText, string memberType, string status);
        Task<Student> AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task SaveChangesAsync();
    }
}
