using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using System.Collections.Generic;
using UescColcicAPI.Services.InputModels;

namespace UescColcicAPI.Services.BD.Interfaces
{
    public interface IStudentsCRUD : IBaseCRUD<StudentViewModel, StudentInputModel>
    {
        int Create(StudentInputModel student);
        void Update(int id, StudentInputModel student);
        void Delete(int id);
        StudentViewModel ReadById(int id);
        IEnumerable<StudentViewModel> ReadAll();
        
    }
}
