using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<Book>> SortBySeriesAsync(string key);
        Task<IEnumerable<Book>> GetPaginatedDatasAsync(int page, int take = 3);
    }
}
