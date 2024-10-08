using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;


namespace UescColcicAPI.Services.BD
{
    public class StudentsCRUD : IStudentsCRUD
    {
        private readonly UescColcicAPIDbContext _context;

        // Construtor para injeção de dependência do DbContext
        public StudentsCRUD(UescColcicAPIDbContext context)
        {
            _context = context;
        }

        public int Create(StudentViewModel studentViewModel)
        {
            var student = new Student
            {
                Registration = studentViewModel.Registration,
                Name = studentViewModel.Name,
                Email = studentViewModel.Email,
                Course = studentViewModel.Course,
                Bio = studentViewModel.Bio
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

        public void Update(int id, StudentViewModel studentViewModel)
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

        public Student ReadById(int id)
        {
            return _context.Students.FirstOrDefault(s => s.StudentId == id);
        }

        public IEnumerable<Student> ReadAll()
        {
            return _context.Students.ToList();
        }
    }
}
