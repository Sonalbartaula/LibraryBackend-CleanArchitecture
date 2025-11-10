using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Model.Dashboard;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using LibraryBackend_CleanArchitecture.Services.Interfaces;

namespace LibraryBackend_CleanArchitecture.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IActivityRepository _activityRepository;

        public StudentService(IStudentRepository studentRepository, IActivityRepository activityRepository)
        {
            _studentRepository = studentRepository;
            _activityRepository = activityRepository;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync(int pageNumber = 1, int pageSize = 20)
        {
            return await _studentRepository.GetAllStudentsAsync(pageNumber, pageSize);
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<IEnumerable<Student>> SearchStudentsAsync(string searchText, string memberType, string status)
        {
            return await _studentRepository.SearchStudentsAsync(searchText, memberType, status);
        }

        public async Task AddStudentAsync(Student student)
        {
            var recallstudent = await _studentRepository.AddStudentAsync(student);
            
            var activity = new Activity
            {
                Type = ActivityType.NewMember,             // Enum type for the activity
                Title = recallstudent.Name,             // Name
                Subtitle = recallstudent.Type.ToString(),           // Type 
                Date = DateTime.UtcNow                       // Timestamp
            };

            await _studentRepository.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _studentRepository.UpdateStudentAsync(student);
            await _studentRepository.SaveChangesAsync();
        }
    }
}
