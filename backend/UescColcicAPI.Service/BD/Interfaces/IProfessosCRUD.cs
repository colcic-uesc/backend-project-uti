using System;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;


namespace UescColcicAPI.Services.BD.Interfaces;

public interface IProfessorsCRUD : IBaseCRUD<ProfessorViewModel,ProfessorInputModel>
{
    int Create(ProfessorInputModel professor);
    void Update(int id, ProfessorInputModel professor);
    void Delete(int id);
    ProfessorViewModel ReadById(int id);
    List<ProfessorViewModel> ReadAll();
    
}
