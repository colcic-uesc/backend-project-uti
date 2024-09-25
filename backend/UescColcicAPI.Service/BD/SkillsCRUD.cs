using System;
using UescColcicAPI.Core;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Services.ViewModel;
using System.Collections.Generic;

namespace UescColcicAPI.Services.BD;

public class SkillsCRUD : ISkillsCRUD
{
    private static readonly List<Skill> Skills = new()
    {
        new Skill {SkillId = 1, Title = "Machine Learning", Description = "Use of algorithms for systems to learn from data."},
        new Skill {SkillId = 1, Title = "Smart Contract Development", Description = "Creation and implementation of smart contracts on platforms such as Ethereum."}
    };
    public int Create(SkillViewModel skillViewModel)
    {
        var skill = new Skill
        {
            Title = skillViewModel.Title,
            Description = skillViewModel.Description
        };
        
        if (Skills.Any(p => p.Title == skill.Title))
        {
            throw new InvalidOperationException($"A skill with the title '{skill.Title}' already exists.");
        }
    
        skill.SkillId = Skills.Any() ? Skills.Max(s => s.SkillId) + 1 : 1;
        Skills.Add(skill);
        return skill.SkillId;
    }

    public void Update(int id, SkillViewModel skillViewModel)
    {
        var skill = Find(id);
        if (skill != null)
        {
            if (Skills.Any(s => s.Title == skillViewModel.Title && s.SkillId != id))
            {
                throw new InvalidOperationException($"Another skill with the title '{skillViewModel.Title}' already exists.");
            }

            skill.Title = skillViewModel.Title;
            skill.Description = skillViewModel.Description;
        }
    }

    public void Delete(int id)
    {
        var skill = Find(id);
        if (skill != null)
        {
            Skills.Remove(skill);
        }
    }
    private Skill ? Find(int id)
    {
        return Skills.FirstOrDefault(s => s.SkillId == id);
    }

    public Skill ReadById(int id)
    {
        if (Find(id) == null)
        {
            throw new InvalidOperationException($"Another skill with the id '{id}' not exists.");
        }
        return Find(id);
    }
    public IEnumerable<Skill> ReadAll()
    {
        return Skills.Select(skill => new Skill
        {
            SkillId = skill.SkillId,
            Title = skill.Title,
            Description = skill.Description
        }).ToList();
    }
}
