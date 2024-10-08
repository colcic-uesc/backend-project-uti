using System;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;

namespace UescColcicAPI.Services.BD.Interfaces;

public interface IProjectsCRUD : IBaseCRUD<ProjectViewModel, Project>
{
        int Create(ProjectViewModel project);
        void Update(int id, ProjectViewModel project);
        void Delete(int id);
        Project ReadById(int id);
        IEnumerable<Project> ReadAll();
        IEnumerable<Project> GetProjectsByProfessorId(int professorId);
}
