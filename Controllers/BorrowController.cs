using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Data;
using Microsoft.AspNetCore.Authorization;  // ADD THIS
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Authorize]  // Must be logged in to borrow/return books
    public class BorrowController : Controller
    {
        private readonly AppDbContext _context;

        public BorrowController(AppDbContext context)
        {
            _context = context;
        }

        // VIEW HISTORY - Show borrowing history for a specific book
        public IActionResult History(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var borrowings = _context.Borrowings
                .Where(b => b.BookId == bookId)
                .OrderByDescending(b => b.BorrowDate)
                .ToList();

            ViewBag.BookTitle = book.Title;
            ViewBag.BookId = bookId;

            return View(borrowings);
        }

        // BORROW - Mark book as borrowed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrow(int bookId)
        {
            try
            {
                var book = _context.Books.Find(bookId);

                if (book == null)
                {
                    TempData["Error"] = "Book not found.";
                    return RedirectToAction("Index", "Books");
                }

                if (book.Status != "Available")
                {
                    TempData["Error"] = "Book is not available.";
                    return RedirectToAction("Index", "Books");
                }

                // Create borrowing record
                var borrowing = new Borrowing
                {
                    BookId = bookId,
                    BorrowDate = DateTime.Now,
                    ReturnDate = null,
                    Status = "Borrowed"
                };

                _context.Borrowings.Add(borrowing);

                // Update book status
                book.Status = "Borrowed";
                book.LastModified = DateTime.Now;

                _context.SaveChanges();

                TempData["Success"] = $"Book '{book.Title}' borrowed successfully!";
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Index", "Books");
            }
        }

        // RETURN - Mark book as returned
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Return(int bookId)
        {
            try
            {
                var book = _context.Books.Find(bookId);

                if (book == null)
                {
                    TempData["Error"] = "Book not found.";
                    return RedirectToAction("Index", "Books");
                }

                if (book.Status != "Borrowed")
                {
                    TempData["Error"] = "Book is not borrowed.";
                    return RedirectToAction("Index", "Books");
                }

                // Find the active borrowing record
                var borrowing = _context.Borrowings
                    .Where(b => b.BookId == bookId && b.Status == "Borrowed")
                    .OrderByDescending(b => b.BorrowDate)
                    .FirstOrDefault();

                if (borrowing != null)
                {
                    borrowing.ReturnDate = DateTime.Now;
                    borrowing.Status = "Returned";
                }

                // Update book status
                book.Status = "Available";
                book.LastModified = DateTime.Now;

                _context.SaveChanges();

                TempData["Success"] = $"Book '{book.Title}' returned successfully!";
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Index", "Books");
            }
        }
    }
}