using System.Collections.Generic;

namespace UescColcicAPI.Core
{
    public class Professor
    {
        public int ProfessorId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Bio { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<Project> Projects { get; set; } = null;
    }
}
