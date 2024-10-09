using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public int Create(ProfessorInputModel professorInputModel)
        {
            var professor = new Professor
            {
                Name = professorInputModel.Name,
                Email = professorInputModel.Email,
                Department = professorInputModel.Department,
                Bio = professorInputModel.Bio
            };

            // Verificar se já existe um professor com o mesmo email
            if (_context.Professors.Any(p => p.Email == professor.Email))
            {
                throw new InvalidOperationException($"A professor with email {professor.Email} already exists.");
            }

            _context.Professors.Add(professor);
            _context.SaveChanges();

            return professor.ProfessorId;
        }

        public void Update(int id, ProfessorInputModel professorInputModel)
        {
            var professor = _context.Professors.FirstOrDefault(p => p.ProfessorId == id);

            if (professor is not null)
            {
                if (_context.Professors.Any(p => p.Email == professorInputModel.Email && p.ProfessorId != id))
                {
                    throw new InvalidOperationException($"Another professor with email {professorInputModel.Email} already exists.");
                }

                professor.Name = professorInputModel.Name;
                professor.Email = professorInputModel.Email;
                professor.Department = professorInputModel.Department;
                professor.Bio = professorInputModel.Bio;

                _context.Professors.Update(professor);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"Professor with id {id} not found.");
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
            else
            {
                throw new InvalidOperationException($"Professor with id {id} not found.");
            }
        }

        public ProfessorViewModel ReadById(int id)
        {
            var professor = _context.Professors
                .Include(p => p.Projects) 
                .FirstOrDefault(p => p.ProfessorId == id);

            if (professor == null)
            {
                throw new InvalidOperationException($"Professor with id {id} not found.");
            }

            
            return new ProfessorViewModel 
            {
                ProfessorId = professor.ProfessorId,
                Name = professor.Name,
                Email = professor.Email,
                Department = professor.Department,
                Bio = professor.Bio,
                Projects = professor.Projects.Select(project => new ProjectViewModel
                {
                    ProjectId = project.ProjectId,
                    Title = project.Title,
                    Description = project.Description,
                    Type = project.Type,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    ProfessorId = professor.ProfessorId
                }).ToList()
            }; 
        }

       public List<ProfessorViewModel> ReadAll()
{
    var professors = _context.Professors.Select(professor => new ProfessorViewModel()
    {
        ProfessorId = professor.ProfessorId,
        Name = professor.Name,
        Email = professor.Email,
        Department = professor.Department,
        Bio = professor.Bio,
        Projects = professor.Projects.Select(project => new ProjectViewModel()
        {
            ProjectId = project.ProjectId,
            Title = project.Title,
            Description = project.Description,
            Type = project.Type,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            ProfessorId = professor.ProfessorId // Adicionando ProfessorId no ProjectViewModel
        }).ToList()

    });

    return professors.ToList(); // Executa a consulta e retorna a lista
}


        

    }
}
