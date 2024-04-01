using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksOnLoan.Data;
using BooksOnLoan.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BooksOnLoan.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly LibraryDbContext _context;

        public TransactionsController(LibraryDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var user = User.Identity.Name;
            var libraryDbContext = _context.Transactions
            .Include(t => t.Book)
            .OrderBy(t => t.Returned);
            return View(await libraryDbContext.ToListAsync());
        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> IndexMember()
        {
            var user = User.Identity.Name;
            var libraryDbContext = _context.Transactions
            .Where(t => t.UserName == user)
            .Include(t => t.Book)
            .OrderBy(t => t.Returned);
            return View(await libraryDbContext.ToListAsync());
        }
        
        [Authorize(Roles = "Admin")]
        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Book)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }
        [Authorize(Roles = "Admin")]
        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Author");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,BookId,UserName,LoanDate,DueDate")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Author", transaction.BookId);
            return View(transaction);
        }
        [Authorize(Roles = "Admin")]
        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Author", transaction.BookId);
            return View(transaction);
        }
        [Authorize(Roles = "Admin")]
        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,BookId,UserName,LoanDate,ReturnDate,DueDate")] Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Author", transaction.BookId);
            return View(transaction);
        }
        [Authorize(Roles = "Admin")]
        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Book)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }
        [Authorize(Roles = "Admin")]
        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }
        [Authorize(Roles = "Member")]
        // POST: Books/Loan
        [HttpPost,ActionName("Return")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (id != transaction.TransactionId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {   
                
                if (transaction != null && transaction.Returned == false)
                {
                    var book = await _context.Books.FindAsync(transaction.BookId);
                    book.Quantity++;
                    _context.Update(book);
                    await _context.SaveChangesAsync();

                    transaction.Returned = true;
                    await _context.SaveChangesAsync();

                    transaction.ReturnDate = DateTime.Now;
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(IndexMember));
                }
            }
            return View(transaction);
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
