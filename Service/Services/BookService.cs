using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Book;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository,
                           IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(BookCreateDto model)
        {
            await _bookRepository.AddAsync(_mapper.Map<Book>(model));
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new NotFoundException("Book not found");
            await _bookRepository.DeleteAsync(book);
        }

        public async Task EditAsync(int id, BookEditDto model)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new NotFoundException("Book not found");
            _mapper.Map(model, book);
            await _bookRepository.EditAsync(book);
        }

        public async Task<IEnumerable<BookDto>> FilterByColorAsync(string color)
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _bookRepository.FindAllWithExpressionAsync(m => m.Color == color));
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _bookRepository.GetAllAsync());
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new NotFoundException("Book not found");
            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> GetBySeriesNumberAsync(int seriesNumber)
        {
            var book = await _bookRepository.FindWithExpressionAsync(b => b.SeriesNumber == seriesNumber);
            if (book == null) throw new NotFoundException("Book not found");
            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetPaginatedDatasAsync(int page)
        {
            var datas = await _bookRepository.GetPaginatedDatasAsync(page);
            return _mapper.Map<IEnumerable<BookDto>>(datas);
        }

        public async Task<IEnumerable<BookDto>> SearchAsync(string str)
        {
            var books = await _bookRepository.FindAllWithExpressionAsync(m => m.Name.Contains(str));
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> SortBySeriesAsync(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException();

            return _mapper.Map<IEnumerable<BookDto>>(await _bookRepository.SortBySeriesAsync(key));
        }
    }
}
