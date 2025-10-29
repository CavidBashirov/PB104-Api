using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace App.Controllers.Client
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> FilterByColor([FromQuery] string color)
        {
            return Ok(await _bookService.FilterByColorAsync(color));
        }

        [HttpGet]
        public async Task<IActionResult> FilterBySeriesNumber([FromQuery] int seriesNumber)
        {
            return Ok(await _bookService.GetBySeriesNumberAsync(seriesNumber));
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string request)
        {
            return Ok(await _bookService.SearchAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> SortBySeries([FromQuery] string? request)
        {
            return Ok(await _bookService.SortBySeriesAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedDatas([FromQuery] int request)
        {
            return Ok(await _bookService.GetPaginatedDatasAsync(request));
        }
    }
}
