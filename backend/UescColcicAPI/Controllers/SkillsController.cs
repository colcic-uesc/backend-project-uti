using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Services.ViewModel;
using System.Collections.Generic;
using System;

namespace UescColcicAPI.Controllers 
{

    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase 
    {
        private readonly ISkillsCRUD _skillsCRUD;

        public SkillsController(ISkillsCRUD skillsCRUD)
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
                if (skill == null)
                {
                    return NotFound($"Skill with ID {id} not found.");
                }

                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Skill
        [HttpPost(Name = "CreateSkill")]
        public ActionResult Create([FromBody] SkillViewModel skillViewModel)
        {
            try
            {
                int newSkillId = _skillsCRUD.Create(skillViewModel);
                return CreatedAtRoute("GetSkill", new { id = newSkillId }, skillViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Skills/5
        [HttpPut("{id}", Name = "UpdateSkill")]
        public ActionResult Update(int id, [FromBody] SkillViewModel skillViewModel)
        {
            try
            {
                var existingSkill = _skillsCRUD.ReadById(id);
                if (existingSkill == null)
                {
                    return NotFound($"Skill with ID {id} not found.");
                }

                _skillsCRUD.Update(id, skillViewModel);
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
                if (skill == null)
                {
                    return NotFound($"Skill with ID {id} not found.");
                }

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
