using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Book;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace App.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService,
                              ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all method is working");
            return Ok(await _bookService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookCreateDto model)
        {
            await _bookService.CreateAsync(model);
            return CreatedAtAction(nameof(Create), "Success created");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogWarning("book deleted");
            await _bookService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _bookService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] BookEditDto model)
        {
            await _bookService.EditAsync(id, model);
            return NoContent();
        }
    }
}
