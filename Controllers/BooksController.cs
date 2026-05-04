using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartStore.MVC.Data;
using SmartStore.MVC.Models;

namespace SmartStore.MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null)
                return NotFound();
            return View(book);  
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book is null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            var existing = await _context.Books.FindAsync(book.BookId);

            if (existing is null)
                return NotFound();

            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Price = book.Price;
            existing.Genre = book.Genre;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book is null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var existing = await _context.Books.FindAsync(id);

            if (existing is null)
                return NotFound();

            _context.Books.Remove(existing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
