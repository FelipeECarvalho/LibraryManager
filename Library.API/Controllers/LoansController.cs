using Library.Application.InputModels.Loans;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v1/loans")]
    public class LoansController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] LoanCreateInputModel model)
        {
            return Created();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put([FromBody] LoanUpdateInputModel model)
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
