using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace UescColcicAPI.Services.BD
{
    public class ProfessorsCRUD : IProfessorsCRUD
    {
        private static readonly List<Professor> Professors = new()
        {
            new Professor { ProfessorId = 1, Name = "Dr. John Doe", Email = "john.doe@university.com", Department = "Computer Science", Bio = "Expert in AI and machine learning" },
            new Professor { ProfessorId = 2, Name = "Dr. Jane Smith", Email = "jane.smith@university.com", Department = "Mathematics", Bio = "Specialist in algebra and number theory" }
        };

        

        public int Create(ProfessorViewModel professorViewModel)
        {
            var professor = new Professor
            {
                Name = professorViewModel.Name,
                Email = professorViewModel.Email,
                Department = professorViewModel.Department,
                Bio = professorViewModel.Bio
            };

            if (Professors.Any(p => p.Email == professor.Email))
            {
                throw new InvalidOperationException($"A professor with email {professor.Email} already exists.");
            }

            professor.ProfessorId = Professors.Any() ? Professors.Max(p => p.ProfessorId) + 1 : 1;
            Professors.Add(professor);
            return professor.ProfessorId;
        }

        public void Update(int id, ProfessorViewModel professorViewModel)
        {
            var professor = ReadById(id);
            if (professor is not null)
            {
                if (Professors.Any(p => p.Email == professorViewModel.Email && p.ProfessorId != id))
                {
                    throw new InvalidOperationException($"Another professor with email {professorViewModel.Email} already exists.");
                }

                professor.Name = professorViewModel.Name;
                professor.Email = professorViewModel.Email;
                professor.Department = professorViewModel.Department;
                professor.Bio = professorViewModel.Bio;
            }
        }

        public void Delete(int id)
        {
            var professor = ReadById(id);
            if (professor != null)
            {
                Professors.Remove(professor);
            }
        }

        public Professor ReadById(int id)
        {
            return Professors.FirstOrDefault(p => p.ProfessorId == id);
        }

        public IEnumerable<Professor> ReadAll()
        {
            return Professors.Select(professor => 
            {

                return professor;
            }).ToList();
        }

        
    }
}
