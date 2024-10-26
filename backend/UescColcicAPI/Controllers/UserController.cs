using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;


namespace UescColcicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
          private readonly IUserCRUD _UsersCRUD;

        public UserController(IUserCRUD UsersCRUD)
        {
            _UsersCRUD = UsersCRUD;
        }

        // GET: api/Users
        [HttpGet(Name = "GetUsers")]
        public ActionResult<IEnumerable<UserViewModel>> Get()
        {
            try
            {
                var Users = _UsersCRUD.ReadAll();
                return Ok(Users); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<UserViewModel> Get(int id)
        {
            try
            {
                var User = _UsersCRUD.ReadById(id);
                return Ok(User); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Users
        [HttpPost(Name = "CreateUser")]
        public ActionResult Create([FromBody] UserInputModel UserInputModel)
        {
            try
            {
                int newUserId = _UsersCRUD.Create(UserInputModel);
                return CreatedAtRoute("GetUser", new { id = newUserId }, UserInputModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpPut("{id}", Name = "UpdateUser")]

        public ActionResult Update(int id, [FromBody] UserInputModel UserInputModel)
        {
            try
            {
                var existingUser = _UsersCRUD.ReadById(id);

                _UsersCRUD.Update(id, UserInputModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpDelete("{id}", Name = "DeleteUser")]
       
        public ActionResult Delete(int id)
        {
            try
            {
                var User = _UsersCRUD.ReadById(id);

                _UsersCRUD.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
    
    }
}
