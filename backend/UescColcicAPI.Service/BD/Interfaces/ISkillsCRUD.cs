using System;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;
using System.Collections.Generic;

namespace UescColcicAPI.Services.BD.Interfaces;
public interface ISkillsCRUD : IBaseCRUD<SkillViewModel, SkillInputModel>
{
    int Create(SkillInputModel skill);
    void Update(int id, SkillInputModel skill);
    void Delete(int id);
    SkillViewModel ReadById(int id);
    IEnumerable<SkillViewModel> ReadAll();
}
