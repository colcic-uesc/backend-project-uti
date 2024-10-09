using System;

namespace UescColcicAPI.Services.InputModels;

public class ProjectInputModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required int ProfessorId { get; set; }
}
