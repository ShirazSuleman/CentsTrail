using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentsTrail.Data;
using CentsTrail.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using CentsTrail.Models.Categories.ViewModels;
using CentsTrail.Models;
using Microsoft.AspNetCore.Identity;

namespace CentsTrail.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private string _currentUserId => GetCurrentUser().Result.Id;

        public CategoriesController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = _context.Categories.Include(b => b.User).Where(c => c.UserId == _currentUserId);
            return View(await categories.ToListAsync());
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Limit,CategoryType")] EditCategoryViewModel viewModel)
        {
            var category = new Category
            {
                Name = viewModel.Name,
                CategoryType = viewModel.CategoryType,
                Limit = viewModel.Limit,
                User = await GetCurrentUser()
            };

            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await GetCategory(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            var categoryViewModel = new EditCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Limit = category.Limit,
                CategoryType = category.CategoryType,
            };

            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Limit,CategoryType")] EditCategoryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            var category = new Category
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Limit = viewModel.Limit,
                CategoryType = viewModel.CategoryType,
                User = await GetCurrentUser()
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(viewModel);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await GetCategory(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var category = await GetCategory(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(long id)
        {
            return _context.Categories.Include(b => b.User).Where(c => c.UserId == _currentUserId).Any(e => e.Id == id);
        }

        private async Task<Category> GetCategory(long id)
        {
            return await _context.Categories.Include(b => b.User)
                                            .Where(c => c.UserId == _currentUserId)
                                            .SingleOrDefaultAsync(m => m.Id == id);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}
