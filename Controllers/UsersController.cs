using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksOnLoan.Data;
using BooksOnLoan.Models;
using Microsoft.AspNetCore.Identity;

namespace BooksOnLoan.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ExtendedUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context,UserManager<ExtendedUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extendedUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (extendedUser == null)
            {
                return NotFound();
            }
            IList<string> roles = await _userManager.GetRolesAsync(extendedUser);

            ViewBag.UserRoles = roles;

            return View(extendedUser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Street,City,Province,PostalCode,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ExtendedUser extendedUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(extendedUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(extendedUser);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extendedUser = await _context.Users.FindAsync(id);
            if (extendedUser == null)
            {
                return NotFound();
            }
            return View(extendedUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Street,City,Province,PostalCode,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ExtendedUser extendedUser)
        {
            if (id != extendedUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(extendedUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtendedUserExists(extendedUser.Id))
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
            return View(extendedUser);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extendedUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (extendedUser == null)
            {
                return NotFound();
            }
            IList<string> roles = await _userManager.GetRolesAsync(extendedUser);

            ViewBag.UserRoles = roles;

            return View(extendedUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var extendedUser = await _context.Users.FindAsync(id);
            if (extendedUser != null)
            {
                _context.Users.Remove(extendedUser);
                
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AddRoleToUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extendedUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (extendedUser == null)
            {
                return NotFound();
            }
            IList<string> roles = await _userManager.GetRolesAsync(extendedUser);

            ViewBag.UserRoles = roles;

            return View(extendedUser);
        }
        [HttpPost, ActionName("AddRoleToUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
            }
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // else if (!await _roleManager.RoleExistsAsync(roleName) && roles.Count == 1)
            // {
            //     await _roleManager.CreateAsync(new IdentityRole(roleName));
                
            // }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        private bool ExtendedUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
