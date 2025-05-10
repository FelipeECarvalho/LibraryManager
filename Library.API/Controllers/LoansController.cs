using AutoMapper;
using Library.Application.DTOs;
using Library.Application.InputModels.Loans;
using Library.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/v1/loans")]
    public class LoansController(ILoanService _loanService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var loans = await _loanService.GetAllAsync();

            if (loans is null)
                return NoContent();

            var dto = _mapper.Map<IList<LoanDto>>(loans);

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);

            if (loan is null)
                return NotFound();

            var dto = _mapper.Map<LoanDto>(loan);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoanCreateInputModel model)
        {
            var loan = _mapper.Map<Loan>(model);

            await _loanService.CreateAsync(loan);

            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] LoanUpdateInputModel model)
        {
            var loan = await _loanService.GetByIdAsync(id);

            if (loan is null)
                return NotFound();

            loan.Update(model.EndDate);

            await _loanService.UpdateAsync(loan);

            return NoContent();
        }

        [HttpPatch("{id:int}/return")]
        public async Task<IActionResult> Return(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);

            if (loan is null)
                return NotFound();

            await _loanService.ReturnAsync(loan);

            var message = DateTime.Now > loan.EndDate
                ? "Book returned past due"
                : "Book returned on time";

            return Ok(message);
        }
    }
}
