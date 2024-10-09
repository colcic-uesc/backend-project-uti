using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UescColcicAPI.Services.BD
{
    public class ProjectsCRUD : IProjectsCRUD
    {
        private readonly UescColcicAPIDbContext _context;

        public ProjectsCRUD(UescColcicAPIDbContext context)
        {
            _context = context;
        }

        public int Create(ProjectInputModel projectInputModel)
        {
            // Verificar se o professor existe
            var professor = _context.Professors.FirstOrDefault(p => p.ProfessorId == projectInputModel.ProfessorId);
            if (professor is null)
            {
                throw new InvalidOperationException($"Professor não existe.");
            }

            var project = new Project
            {
                Title = projectInputModel.Title,
                Description = projectInputModel.Description,
                Type = projectInputModel.Type,
                StartDate = projectInputModel.StartDate,
                EndDate = projectInputModel.EndDate,
                ProfessorId = projectInputModel.ProfessorId 
            };

            if (_context.Projects.Any(p => p.Title == project.Title))
            {
                throw new InvalidOperationException($"A project with the title '{project.Title}' already exists.");
            }

            _context.Projects.Add(project);
            _context.SaveChanges();

            return project.ProjectId; 
        }

        public void Update(int id, ProjectInputModel projectInputModel)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project != null)
            {
                // Verificar se já existe outro projeto com o mesmo título
                if (_context.Projects.Any(p => p.Title == projectInputModel.Title && p.ProjectId != id))
                {
                    throw new InvalidOperationException($"Another project with the title '{projectInputModel.Title}' already exists.");
                }

                project.Title = projectInputModel.Title;
                project.Description = projectInputModel.Description;
                project.Type = projectInputModel.Type;
                project.StartDate = projectInputModel.StartDate;
                project.EndDate = projectInputModel.EndDate;
                project.ProfessorId = projectInputModel.ProfessorId;

                _context.Projects.Update(project);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"Project with id {id} not found.");
            }
        }

        public void Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"Project with id {id} not found.");
            }
        }

        public Project ReadById(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project == null)
            {
                throw new InvalidOperationException($"Project with id {id} not found.");
            }
            return project;
        }

        public List<ProjectViewModel> ReadAll()
        {
            var projects = _context.Projects.Select(p => new ProjectViewModel
            {
                ProjectId = p.ProjectId,
                Title = p.Title,
                Description = p.Description,
                Type = p.Type,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProfessorId = p.ProfessorId
            });
            return projects.ToList();
        }

        public List<ProjectViewModel> GetProjectsByProfessorId(int professorId)
        {
            var projects = _context.Projects.Select(p => new ProjectViewModel
            {
                ProjectId = p.ProjectId,
                Title = p.Title,
                Description = p.Description,
                Type = p.Type,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                ProfessorId = p.ProfessorId
            });
            return projects.Where(p => p.ProfessorId == professorId).ToList();
        }
    }
}
