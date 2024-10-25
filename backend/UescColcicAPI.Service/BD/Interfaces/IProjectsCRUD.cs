using System;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;

namespace UescColcicAPI.Services.BD.Interfaces;

public interface IProjectsCRUD : IBaseCRUD<ProjectViewModel, ProjectInputModel>
{
        int Create(ProjectInputModel project);
        void Update(int id, ProjectInputModel project);
        void Delete(int id);
        ProjectViewModel ReadById(int id);
        List<ProjectViewModel> ReadAll();
        List<ProjectViewModel> GetProjectsByProfessorId(int professorId);
}
