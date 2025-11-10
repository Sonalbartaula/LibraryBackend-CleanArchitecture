using LibraryBackend_CleanArchitecture.Model;

namespace LibraryBackend_CleanArchitecture.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync(int pageNumber = 1, int pageSize = 20);
        Task<Student?> GetStudentByIdAsync(int id);
        Task<IEnumerable<Student>> SearchStudentsAsync(string searchText, string memberType, string status);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
    }
}
