using AutoMapper;
using WebApplication1.Data;

namespace WebApplication1.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly PostgreSqlContext postgreSqlContext;
        private readonly IMapper mapper;

        public BookService(PostgreSqlContext context, IMapper mapper)
        {
            postgreSqlContext = context;
            this.mapper = mapper;
        }
        /*public async Task<List<Book>> AddBook(Book book)
        {
            postgreSqlContext.Books.Add(book);
            await postgreSqlContext.SaveChangesAsync();
            return await postgreSqlContext.Books.ToListAsync(); ;
        }

        public async Task<List<Book>?> DeleteBook(int id)
        {
            var book = await postgreSqlContext.Books.FindAsync(id);
            if (book is null)
                return null;

            postgreSqlContext.Books.Remove(book);
            await postgreSqlContext.SaveChangesAsync();
            return await postgreSqlContext.Books.ToListAsync(); ;
        }*/

        public async Task<List<GetBookDto>> GetAllBooks()
        {
            return await postgreSqlContext.Books.Include(b => b.Category).Select(b => mapper.Map<GetBookDto>(b)).ToListAsync();
        }

        public async Task<GetBookDto?> GetBook(int id)
        {
            var book = postgreSqlContext.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);

            if (book is null)
               return null;

            return mapper.Map<GetBookDto>(book);
        }
        

        /* public async Task<List<Book>?> UpdateBook(Book bookData)
         {
             var book = await postgreSqlContext.Books.FindAsync(bookData.Id);
             if (book is null)
                 return null;

             book.Title = bookData.Title;

             await postgreSqlContext.SaveChangesAsync();

             return await postgreSqlContext.Books.ToListAsync();
         }*/
    }
}
