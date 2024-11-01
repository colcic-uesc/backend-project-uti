using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;


namespace UescColcicAPI.Services.BD
{
    public class StudentsCRUD : IStudentsCRUD
    {
        private readonly UescColcicAPIDbContext _context;

        
        public StudentsCRUD(UescColcicAPIDbContext context)
        {
            _context = context;
        }

        public int Create(StudentInputModel studentViewModel)
        {
            var student = new Student
            {
                Registration = studentViewModel.Registration,
                Name = studentViewModel.Name,
                Email = studentViewModel.Email,
                Course = studentViewModel.Course,
                Bio = studentViewModel.Bio,
                UserId = studentViewModel.UserId
            };

            // Verificar se já existe um estudante com o mesmo registro
            if (_context.Students.Any(s => s.Registration == student.Registration))
            {
                throw new InvalidOperationException($"A student with registration {student.Registration} already exists.");
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return student.StudentId; // O EF Core atualizará automaticamente o ID após o SaveChanges
        }

        public void Update(int id, StudentInputModel studentViewModel)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id);
            if (student != null)
            {
                // Verificar se já existe outro estudante com o mesmo registro
                if (_context.Students.Any(s => s.Registration == studentViewModel.Registration && s.StudentId != id))
                {
                    throw new InvalidOperationException($"Another student with registration {studentViewModel.Registration} already exists.");
                }

                student.Name = studentViewModel.Name;
                student.Email = studentViewModel.Email;
                student.Registration = studentViewModel.Registration;
                student.Course = studentViewModel.Course;
                student.Bio = studentViewModel.Bio;
                student.UserId = studentViewModel.UserId;

                _context.Students.Update(student);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public StudentViewModel ReadById(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                throw new InvalidOperationException($"Student with id {id} not found.");
            }
            return new StudentViewModel
            {
                StudentId = student.StudentId,
                Registration = student.Registration,
                Name = student.Name,
                Email = student.Email,
                Course = student.Course,
                Bio = student.Bio
            };
        }

        public IEnumerable<StudentViewModel> ReadAll()
        {
            var student = _context.Students.ToList();
            return student.Select(s => new StudentViewModel
            {
                StudentId = s.StudentId,
                Registration = s.Registration,
                Name = s.Name,
                Email = s.Email,
                Course = s.Course,
                Bio = s.Bio
            });
        }
    }
}
