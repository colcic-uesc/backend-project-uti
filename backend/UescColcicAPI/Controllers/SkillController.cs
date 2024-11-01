using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;
using Microsoft.AspNetCore.Authorization;

namespace UescColcicAPI.Controllers 
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize (Roles = "Admin")]
    public class SkillController : ControllerBase 
    {
        private readonly ISkillsCRUD _skillsCRUD;

        public SkillController(ISkillsCRUD skillsCRUD)
        {
            _skillsCRUD = skillsCRUD;
        }

        // GET: api/Skills
        [HttpGet(Name = "GetSkills")]
        public ActionResult<IEnumerable<SkillViewModel>> Get()
        {
            try
            {
                var skills = _skillsCRUD.ReadAll();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

         // GET: api/Skills/5
        [HttpGet("{id}", Name = "GetSkill")]
        public ActionResult<SkillViewModel> Get(int id)
        {
            try
            {
                var skill = _skillsCRUD.ReadById(id);
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Skill
        [HttpPost(Name = "CreateSkill")]
        public ActionResult Create([FromBody] SkillInputModel skillInputModel)
        {
            try
            {
                int newSkillId = _skillsCRUD.Create(skillInputModel);
                return CreatedAtRoute("GetSkill", new { id = newSkillId }, skillInputModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Skills/5
        [HttpPut("{id}", Name = "UpdateSkill")]
        public ActionResult Update(int id, [FromBody] SkillInputModel skillInputModel)
        {
            try
            {
                var existingSkill = _skillsCRUD.ReadById(id);
                _skillsCRUD.Update(id, skillInputModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}", Name = "DeleteSkill")]
        public ActionResult Delete(int id)
        {
            try
            {
                var skill = _skillsCRUD.ReadById(id);
                _skillsCRUD.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
