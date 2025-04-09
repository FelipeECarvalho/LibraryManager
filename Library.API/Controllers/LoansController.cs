using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v1/loans")]
    public class LoansController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return Created();
        }

        [HttpPut("{id:int}/return")]
        public IActionResult Return(int id) 
        {
            return NoContent();
        }
    }
}
