using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;


namespace UescColcicAPI.Services.BD
{
    public class SkillsCRUD : ISkillsCRUD
    {
        private readonly UescColcicAPIDbContext _context;

        // Construtor para injeção de dependência do DbContext
        public SkillsCRUD(UescColcicAPIDbContext context)
        {
            _context = context;
        }

        public int Create(SkillViewModel skillViewModel)
        {
            var skill = new Skill
            {
                Title = skillViewModel.Title,
                Description = skillViewModel.Description
            };

            // Verificar se já existe uma skill com o mesmo título
            if (_context.Skills.Any(s => s.Title == skill.Title))
            {
                throw new InvalidOperationException($"A skill with the title '{skill.Title}' already exists.");
            }

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return skill.SkillId; // O EF Core atualizará automaticamente o ID após o SaveChanges
        }

        public void Update(int id, SkillViewModel skillViewModel)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.SkillId == id);
            if (skill != null)
            {
                // Verificar se já existe outra skill com o mesmo título
                if (_context.Skills.Any(s => s.Title == skillViewModel.Title && s.SkillId != id))
                {
                    throw new InvalidOperationException($"Another skill with the title '{skillViewModel.Title}' already exists.");
                }

                skill.Title = skillViewModel.Title;
                skill.Description = skillViewModel.Description;

                _context.Skills.Update(skill);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var skill = _context.Skills.FirstOrDefault(s => s.SkillId == id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                _context.SaveChanges();
            }
        }

        public Skill ReadById(int id)
        {
            return _context.Skills.FirstOrDefault(s => s.SkillId == id);
        }

        public IEnumerable<Skill> ReadAll()
        {
            return _context.Skills.ToList();
        }
    }
}
