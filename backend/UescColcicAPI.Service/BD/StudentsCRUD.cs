using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace UescColcicAPI.Services.BD
{
    public class StudentsCRUD : IStudentsCRUD
    {
        private static readonly List<Student> Students = new()
        {
            new Student { StudentId = 1, Registration = "REG123", Name = "Douglas", Email = "douglas.cic@uesc.br", Course = "Computer Science", Bio = "A passionate computer science student." },
            new Student { StudentId = 2, Registration = "REG124", Name = "Estevão", Email = "estevao.cic@uesc.br", Course = "Software Engineering", Bio = "Software enthusiast and problem solver." },
            new Student { StudentId = 3, Registration = "REG125", Name = "Gabriel", Email = "gabriel.cic@uesc.br", Course = "Information Systems", Bio = "Interested in data and business systems." },
            new Student { StudentId = 4, Registration = "REG126", Name = "Gabriela", Email = "gabriela.cic@uesc.br", Course = "Artificial Intelligence", Bio = "Exploring AI and machine learning." }
        };

        public int Create(StudentViewModel studentViewModel)
        {
            // Converte o StudentViewModel para Student
            var student = new Student
            {
                Registration = studentViewModel.Registration,
                Name = studentViewModel.Name,
                Email = studentViewModel.Email,
                Course = studentViewModel.Course,
                Bio = studentViewModel.Bio
            };

            // Verifica se o registro já existe
            if (Students.Any(s => s.Registration == student.Registration))
            {
                throw new InvalidOperationException($"A student with registration {student.Registration} already exists.");
            }

            // Gera automaticamente um ID para o novo estudante
            student.StudentId = Students.Any() ? Students.Max(s => s.StudentId) + 1 : 1;
            Students.Add(student);

            return student.StudentId;
        }

        public void Update(int id, StudentViewModel studentViewModel)
        {
            var student = this.Find(id);
            if (student is not null)
            {
                // Verifica se outro estudante já usa o mesmo registration
                if (Students.Any(s => s.Registration == studentViewModel.Registration && s.StudentId != id))
                {
                    throw new InvalidOperationException($"Another student with registration {studentViewModel.Registration} already exists.");
                }

                // Atualiza os campos
                student.Name = studentViewModel.Name;
                student.Email = studentViewModel.Email;
                student.Registration = studentViewModel.Registration;
                student.Course = studentViewModel.Course;
                student.Bio = studentViewModel.Bio;
            }
        }

        public void Delete(int id)
        {
            var student = this.Find(id);
            if (student is not null)
            {
                Students.Remove(student);
            }
        }

        public Student ReadById(int id)
        {
            var student = this.Find(id);
            if (student is not null)
            {
                // Converte Student para StudentViewModel
                return new Student
                {
                    StudentId = student.StudentId,
                    Registration = student.Registration,
                    Name = student.Name,
                    Email = student.Email,
                    Course = student.Course,
                    Bio = student.Bio
                };
            }
            return null;
        }

        public IEnumerable<Student> ReadAll()
        {
            // Converte a lista de Students para StudentViewModels
            return Students.Select(student => new Student
            {
                StudentId = student.StudentId,
                Registration = student.Registration,
                Name = student.Name,
                Email = student.Email,
                Course = student.Course,
                Bio = student.Bio
            }).ToList();
        }

        // Busca um estudante por ID
        private Student? Find(int id)
        {
            return Students.FirstOrDefault(x => x.StudentId == id);
        }
    }
}
