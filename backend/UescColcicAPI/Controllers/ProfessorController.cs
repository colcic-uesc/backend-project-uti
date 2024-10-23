using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace UescColcicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorsController : ControllerBase
    {
        private readonly IProfessorsCRUD _professorsCRUD;

        public ProfessorsController(IProfessorsCRUD professorsCRUD)
        {
            _professorsCRUD = professorsCRUD;
        }

        // GET: api/Professors
        [HttpGet(Name = "GetProfessors")]
        public ActionResult<IEnumerable<ProfessorViewModel>> Get()
        {
            try
            {
                var professors = _professorsCRUD.ReadAll();
                return Ok(professors); // A lógica de conversão para ProfessorViewModel está no service
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Professors/5
        [HttpGet("{id}", Name = "GetProfessor")]
        public ActionResult<ProfessorViewModel> Get(int id)
        {
            try
            {
                var professor = _professorsCRUD.ReadById(id);
                if (professor == null)
                {
                    return NotFound($"Professor with ID {id} not found.");
                }

                return Ok(professor); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Professors
        [HttpPost(Name = "CreateProfessor")]
        [Authorize]
        public ActionResult Create([FromBody] ProfessorInputModel professorInputModel)
        {
            try
            {
                int newProfessorId = _professorsCRUD.Create(professorInputModel);
                return CreatedAtRoute("GetProfessor", new { id = newProfessorId }, professorInputModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpPut("{id}", Name = "UpdateProfessor")]
        [Authorize]
        public ActionResult Update(int id, [FromBody] ProfessorInputModel professorInputModel)
        {
            try
            {
                var existingProfessor = _professorsCRUD.ReadById(id);
                if (existingProfessor == null)
                {
                    return NotFound($"Professor with ID {id} not found.");
                }

                _professorsCRUD.Update(id, professorInputModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpDelete("{id}", Name = "DeleteProfessor")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                var professor = _professorsCRUD.ReadById(id);
                if (professor == null)
                {
                    return NotFound($"Professor with ID {id} not found.");
                }

                _professorsCRUD.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    }