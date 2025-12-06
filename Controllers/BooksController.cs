using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using Microsoft.AspNetCore.Authorization;  // ADD THIS
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Authorize] // ADD THIS - Now ALL actions require login
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET - List all books with search/filter and pagination
        public IActionResult Index(string searchTitle, string searchAuthor, string filterStatus, int pageNumber = 1)
        {
            // PAGINATION SETTINGS
            const int pageSize = 10;  // Show 10 books per page

            // Start with ALL books
            var booksQuery = _context.Books.AsQueryable();

            // FILTER 1: Search by Title
            if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(searchTitle));
            }

            // FILTER 2: Search by Author Name
            if (!string.IsNullOrWhiteSpace(searchAuthor))
            {
                booksQuery = booksQuery.Where(b => b.AuthorName.Contains(searchAuthor));
            }

            // FILTER 3: Filter by Status
            if (!string.IsNullOrWhiteSpace(filterStatus) && filterStatus != "All")
            {
                booksQuery = booksQuery.Where(b => b.Status == filterStatus);
            }

            // COUNT TOTAL BOOKS (before pagination)
            var totalBooks = booksQuery.Count();

            // CALCULATE TOTAL PAGES
            var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);

            // ENSURE PAGE NUMBER IS VALID
            if (pageNumber < 1) pageNumber = 1;
            if (pageNumber > totalPages && totalPages > 0) pageNumber = totalPages;

            // APPLY PAGINATION - Skip and Take
            var booksOnCurrentPage = booksQuery
                .OrderBy(b => b.Title)  // Sort by title (important for consistent pagination)
                .Skip((pageNumber - 1) * pageSize)  // Skip previous pages
                .Take(pageSize)  // Take only current page
                .ToList();

            // STATS
            ViewBag.BookCount = totalBooks;  // Total books (not just on this page)

            var allMembers = _context.Members.ToList();
            ViewBag.MemberCount = allMembers?.Count ?? 0;

            // SEARCH VALUES (to preserve in form)
            ViewBag.SearchTitle = searchTitle;
            ViewBag.SearchAuthor = searchAuthor;
            ViewBag.FilterStatus = filterStatus;

            // PAGINATION DATA (to display page numbers)
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalBooks = totalBooks;

            return View(booksOnCurrentPage);
        }

        // CREATE - Display form (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - Handle form submission (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,AuthorID,AuthorName,BookPrices,Status")] Book book)
        {
            // Remove manual validation - use ModelState instead
            if (ModelState.IsValid)
            {
                try
                {
                    //book.Status = "Available";
                    book.LastModified = DateTime.Now;

                    _context.Books.Add(book);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error saving book: {ex.Message}");
                }
            }

            // If we got here, something failed, redisplay form
            return View(book);
        }

        // DETAILS - Show single book
        //public IActionResult Details(int id)
        //{
        //    var book = _context.Books.Find(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(book);
        //}

        // EDIT - Display edit form (GET)
        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // EDIT - Handle edit submission (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("BookID,Title,AuthorID,AuthorName,BookPrices,Status")] Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingBook = _context.Books.Find(book.BookID);
                    if (existingBook == null)
                    {
                        return NotFound();
                    }

                    existingBook.Title = book.Title;
                    existingBook.AuthorID = book.AuthorID;
                    existingBook.AuthorName = book.AuthorName;
                    existingBook.BookPrices = book.BookPrices;
                    existingBook.Status = book.Status;
                    existingBook.LastModified = DateTime.Now;

                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating book: {ex.Message}");
                }
            }

            return View(book);
        }

        // DELETE - Show delete confirmation (GET)
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // DELETE - Handle delete (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var book = _context.Books.Find(id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log error or handle it
                return RedirectToAction(nameof(Index));
            }
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }
    }
}