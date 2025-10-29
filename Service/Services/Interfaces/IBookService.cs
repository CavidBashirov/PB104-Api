using Service.DTOs.Book;

namespace Service.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto> GetByIdAsync(int id);
        Task CreateAsync(BookCreateDto model);
        Task EditAsync(int id, BookEditDto model);
        Task DeleteAsync(int id);
        Task<IEnumerable<BookDto>> FilterByColorAsync(string color);
        Task<BookDto> GetBySeriesNumberAsync(int seriesNumber);
        Task<IEnumerable<BookDto>> SearchAsync(string str);
        Task<IEnumerable<BookDto>> SortBySeriesAsync(string key);
        Task<IEnumerable<BookDto>> GetPaginatedDatasAsync(int page);
    }
}
