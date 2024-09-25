using System;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModel;
using System.Collections.Generic;

namespace UescColcicAPI.Services.BD.Interfaces;
public interface ISkillsCRUD : IBaseCRUD<SkillViewModel, SkillViewModel>
{
    int Create(SkillViewModel skill);
    void Update(int id, SkillViewModel skill);
    void Delete(int id);
    Skill ReadById(int id);
    IEnumerable<Skill> ReadAll();
    /*
    IEnumerable<Skill> GetProjectsByProjectId(int projectId); 
    IEnumerable<Skill> GetProjectsByStudentId(int studentId); 
    */
}
