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
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost(Name = "CreateStudent")]
        public ActionResult Post([FromBody] StudentInputModel studentInputModel)
        {
            try
            {
                int newStudentId = _studentsCRUD.Create(studentInputModel);
                return CreatedAtRoute("GetStudent", new { id = newStudentId }, studentInputModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}", Name = "UpdateStudent")]
        public ActionResult Update(int id, [FromBody] StudentInputModel studentInputModel)
        {
            try
            {
                var existingStudent = _studentsCRUD.ReadById(id);
                _studentsCRUD.Update(id, studentInputModel);

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
