using System;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModel;
using UescColcicAPI.Services.ViewModels;

namespace UescColcicAPI.Services.BD.Interfaces;

public interface IProfessorsCRUD : IBaseCRUD<ProfessorViewModel,Professor>
{
    int Create(ProfessorViewModel professor);
    void Update(int id, ProfessorViewModel professor);
    void Delete(int id);
    Professor ReadById(int id);
    IEnumerable<Professor> ReadAll();
    
}
