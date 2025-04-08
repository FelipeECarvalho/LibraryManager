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

        [HttpPut]
        public IActionResult Return() 
        {
            return NoContent();
        }
    }
}
