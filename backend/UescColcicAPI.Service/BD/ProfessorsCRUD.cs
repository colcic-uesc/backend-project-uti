using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace UescColcicAPI.Services.BD
{
    public class ProfessorsCRUD : IProfessorsCRUD
    {
        private readonly UescColcicAPIDbContext _context;
        private readonly IProjectsCRUD _projectsCRUD;

        public ProfessorsCRUD(UescColcicAPIDbContext context, IProjectsCRUD projectsCRUD)
        {
            _context = context;
            _projectsCRUD = projectsCRUD;
        }

        public int Create(ProfessorViewModel professorViewModel)
        {
            var professor = new Professor
            {
                Name = professorViewModel.Name,
                Email = professorViewModel.Email,
                Department = professorViewModel.Department,
                Bio = professorViewModel.Bio
            };

            // Verificar se já existe um professor com o mesmo email
            if (_context.Professors.Any(p => p.Email == professor.Email))
            {
                throw new InvalidOperationException($"A professor with email {professor.Email} already exists.");
            }

            _context.Professors.Add(professor);
            _context.SaveChanges();

            return professor.ProfessorId; // O EF Core atualizará automaticamente o ID após o SaveChanges
        }

        public void Update(int id, ProfessorViewModel professorViewModel)
        {
            var professor = _context.Professors.FirstOrDefault(p => p.ProfessorId == id);

            if (professor is not null)
            {
                if (_context.Professors.Any(p => p.Email == professorViewModel.Email && p.ProfessorId != id))
                {
                    throw new InvalidOperationException($"Another professor with email {professorViewModel.Email} already exists.");
                }

                professor.Name = professorViewModel.Name;
                professor.Email = professorViewModel.Email;
                professor.Department = professorViewModel.Department;
                professor.Bio = professorViewModel.Bio;

                _context.Professors.Update(professor);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var professor = _context.Professors.FirstOrDefault(p => p.ProfessorId == id);

            if (professor != null)
            {
                _context.Professors.Remove(professor);
                _context.SaveChanges();
            }
        }

        public Professor ReadById(int id)
        {
            var professor = _context.Professors
                .Include(p => p.Projects) 
                .FirstOrDefault(p => p.ProfessorId == id);

            if (professor != null)
            {
                professor.Projects = _projectsCRUD.GetProjectsByProfessorId(id);
            }

            return professor;
        }

        public IEnumerable<Professor> ReadAll()
        {
            var professors = _context.Professors.ToList();

            foreach (var professor in professors)
            {
                professor.Projects = _projectsCRUD.GetProjectsByProfessorId(professor.ProfessorId);
            }

            return professors;
        }
    }
}
