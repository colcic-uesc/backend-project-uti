using System;

namespace UescColcicAPI.Core;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Roles { get; set; }   

    public virtual List<Professor> Professores { get; set; } = new List<Professor>();
    public virtual List<Student> Students { get; set; } = new List<Student>();
}
