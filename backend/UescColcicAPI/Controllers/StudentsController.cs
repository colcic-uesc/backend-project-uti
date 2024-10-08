using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Services.ViewModels;

namespace UescColcicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsCRUD _studentsCRUD;

        public StudentsController(IStudentsCRUD studentsCRUD)
        {
            _studentsCRUD = studentsCRUD;
        }

        [HttpGet(Name = "GetStudents")]
        public ActionResult<IEnumerable<StudentViewModel>> Get()
        {
            try
            {
                var students = _studentsCRUD.ReadAll();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public ActionResult<StudentViewModel> Get(int id)
        {
            try
            {
                var student = _studentsCRUD.ReadById(id);
                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost(Name = "CreateStudent")]
        public ActionResult Post([FromBody] StudentViewModel studentViewModel)
        {
            try
            {
                // Encaminha a criação para o serviço
                int newStudentId = _studentsCRUD.Create(studentViewModel);

                return CreatedAtRoute("GetStudent", new { id = newStudentId }, studentViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}", Name = "UpdateStudent")]
        public ActionResult Update(int id, [FromBody] StudentViewModel studentViewModel)
        {
            try
            {
                var existingStudent = _studentsCRUD.ReadById(id);
                if (existingStudent == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }

                // Encaminha a atualização para o serviço
                _studentsCRUD.Update(id, studentViewModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}", Name = "DeleteStudent")]
        public ActionResult Delete(int id)
        {
            try
            {
                var student = _studentsCRUD.ReadById(id);
                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }

                // Encaminha a exclusão para o serviço
                _studentsCRUD.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
