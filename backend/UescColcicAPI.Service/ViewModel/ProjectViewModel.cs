using System;

namespace UescColcicAPI.Services.ViewModel;

public class ProjectViewModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required int ProfessorId { get; set; }
}
