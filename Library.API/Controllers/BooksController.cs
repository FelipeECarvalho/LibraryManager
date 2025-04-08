using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v1/books")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) 
        {
            return Ok(new { id });
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Created();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id) 
        {
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id)
        {
            return NoContent();
        }
    }
}
