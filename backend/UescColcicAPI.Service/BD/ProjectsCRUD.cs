using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UescColcicAPI.Services.BD
{
    public class ProjectsCRUD : IProjectsCRUD
    {
        private static readonly List<Project> Projects = new()
        {
            new Project { ProjectId = 1, Title = "AI Research", Description = "Research on AI applications",Type= "A", StartDate = DateTime.Now.AddMonths(-6), EndDate = DateTime.Now.AddMonths(6)},
            new Project { ProjectId = 2, Title = "Blockchain Initiative", Description = "Exploring blockchain for decentralized systems",Type= "B", StartDate = DateTime.Now.AddMonths(-3), EndDate = DateTime.Now.AddMonths(9) }
        };

        public int Create(ProjectViewModel projectViewModel)
        {
            var project = new Project
            {
                Title = projectViewModel.Title,
                Description = projectViewModel.Description,
                Type = projectViewModel.Type,
                StartDate = projectViewModel.StartDate,
                EndDate = projectViewModel.EndDate
            };

            if (Projects.Any(p => p.Title == project.Title))
            {
                throw new InvalidOperationException($"A project with the title '{project.Title}' already exists.");
            }

            project.ProjectId = Projects.Any() ? Projects.Max(p => p.ProjectId) + 1 : 1;
            Projects.Add(project);
            return project.ProjectId;
        }

        public void Update(int id, ProjectViewModel projectViewModel)
        {
            var project = ReadById(id);
            if (project != null)
            {
                if (Projects.Any(p => p.Title == projectViewModel.Title && p.ProjectId != id))
                {
                    throw new InvalidOperationException($"Another project with the title '{projectViewModel.Title}' already exists.");
                }

                project.Title = projectViewModel.Title;
                project.Description = projectViewModel.Description;
                project.Type = projectViewModel.Type;
                project.StartDate = projectViewModel.StartDate;
                project.EndDate = projectViewModel.EndDate;
            }
        }

        public void Delete(int id)
        {
            var project = ReadById(id);
            if (project != null)
            {
                Projects.Remove(project);
            }
        }

        public Project ReadById(int id)
        {
            return Projects.FirstOrDefault(p => p.ProjectId == id);
        }

        public IEnumerable<Project> ReadAll()
        {
           return Projects.Select(professor => 
            {
                return professor;
            }).ToList();
        }
    }
}
