using System;

namespace UescColcicAPI.Core;

public class StudentSkill
{
    public Guid StudentSkillId { get; set;}
    public string ? Weight { get; set;}
    public Guid IdStudent { get; set;}
    public Student Student { get; set;}
    public Guid IdSkill { get; set;}
    public Skill Skill { get; set;}
}
