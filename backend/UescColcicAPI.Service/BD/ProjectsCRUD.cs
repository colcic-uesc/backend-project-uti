using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;


namespace UescColcicAPI.Services.BD
{
    public class ProjectsCRUD : IProjectsCRUD
    {
        private readonly UescColcicAPIDbContext _context;

        public ProjectsCRUD(UescColcicAPIDbContext context)
        {
            _context = context;
        }

        public int Create(ProjectViewModel projectViewModel)
        {
            var project = new Project
            {
                Title = projectViewModel.Title,
                Description = projectViewModel.Description,
                Type = projectViewModel.Type,
                StartDate = projectViewModel.StartDate,
                EndDate = projectViewModel.EndDate,
                ProfessorId = projectViewModel.ProfessorId
            };

            // Verificar se já existe um projeto com o mesmo título
            if (_context.Projects.Any(p => p.Title == project.Title))
            {
                throw new InvalidOperationException($"A project with the title '{project.Title}' already exists.");
            }

            _context.Projects.Add(project);
            _context.SaveChanges();

            return project.ProjectId; // O EF Core atualizará automaticamente o ID após o SaveChanges
        }

        public void Update(int id, ProjectViewModel projectViewModel)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project != null)
            {
                // Verificar se já existe outro projeto com o mesmo título
                if (_context.Projects.Any(p => p.Title == projectViewModel.Title && p.ProjectId != id))
                {
                    throw new InvalidOperationException($"Another project with the title '{projectViewModel.Title}' already exists.");
                }

                project.Title = projectViewModel.Title;
                project.Description = projectViewModel.Description;
                project.Type = projectViewModel.Type;
                project.StartDate = projectViewModel.StartDate;
                project.EndDate = projectViewModel.EndDate;
                project.ProfessorId = projectViewModel.ProfessorId;

                _context.Projects.Update(project);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
        }

        public Project ReadById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.ProjectId == id);
        }

        public IEnumerable<Project> ReadAll()
        {
            return _context.Projects.ToList();
        }

        public IEnumerable<Project> GetProjectsByProfessorId(int professorId)
        {
            return _context.Projects.Where(p => p.ProfessorId == professorId).ToList();
        }
    }
}
