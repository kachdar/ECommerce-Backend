namespace WebApplication1.Services.BookService
{
    public interface IBookService
    {
        Task<List<GetBookDto>> GetAllBooks();
        Task<GetBookDto?> GetBook(int id);
        /*Task<List<Book>> AddBook(Book book);
        Task<List<Book>?> UpdateBook(Book bookData);
        Task<List<Book>?> DeleteBook(int id);*/
    }
}
