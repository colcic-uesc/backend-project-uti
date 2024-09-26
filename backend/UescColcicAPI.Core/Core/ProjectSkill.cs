using System;

namespace UescColcicAPI.Core.Core;

public class ProjectSkill
{
    public Guid ProjectSkillId { get; set;}
    public string ? Weight { get; set;}
    public Guid IdProject { get; set;}
    public Project Project { get; set;}
    public Guid IdSkill { get; set;}
    public Skill Skill { get; set;}
}
