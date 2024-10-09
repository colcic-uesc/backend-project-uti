namespace UescColcicAPI.Services.ViewModels;

public class ProfessorViewModel
{
    public int ProfessorId { get; set; }    
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Department { get; set; }
    public string? Bio { get; set; }
    public List<ProjectViewModel> Projects { get; set; } = null;

}
