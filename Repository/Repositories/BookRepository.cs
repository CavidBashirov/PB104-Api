using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BookRepository : BaseRepository<Book> , IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetPaginatedDatasAsync(int page, int take = 3)
        {
            var datas = await _context.Books.Skip((page * take) - take).Take(take).ToListAsync();
            return datas;
        }

        public async Task<IEnumerable<Book>> SortBySeriesAsync(string key)
        {
            if(key == "asc")
                return await _context.Books.OrderBy(m => m.SeriesNumber).ToListAsync();

            return await _context.Books.OrderByDescending(m => m.SeriesNumber).ToListAsync();

        }
    }
}
