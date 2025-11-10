using LibraryBackend_CleanArchitecture.Data;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend_CleanArchitecture.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LibraryDbContext _context;

        public StudentRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync(int pageNumber = 1, int pageSize = 10)
        {
            pageSize = Math.Min(pageSize, 100);
            return await _context.Students
           .OrderBy(b => b.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> SearchStudentsAsync(string searchText, string memberType, string status)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(s =>
                    s.Name.Contains(searchText) ||
                    s.Contact.Contains(searchText));
            }

            if (!string.IsNullOrEmpty(memberType) &&
                Enum.TryParse<MemberType>(memberType, true, out var parsedMemberType))
            {
                query = query.Where(s => s.Type == parsedMemberType);
            }

            if (!string.IsNullOrEmpty(status) &&
                Enum.TryParse<Status>(status, true, out var parsedStatus))
            {
                query = query.Where(s => s.Status == parsedStatus);
            }

            return await query.ToListAsync();
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            return student;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
