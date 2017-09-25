using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CentsTrail.Data;
using CentsTrail.Models.BankAccounts;
using CentsTrail.Models.BankAccounts.ViewModels;
using CentsTrail.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CentsTrail.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class BankAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private string _currentUserId => GetCurrentUser().Result.Id;

        public BankAccountsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            var bankAccounts = _context.BankAccounts.Include(b => b.User).Where(ba => ba.UserId == _currentUserId);
            return View(await bankAccounts.ToListAsync());
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BankAccountType")] EditBankAccountViewModel bankAccountViewModel)
        {
            var bankAccount = new BankAccount
            {
                Name = bankAccountViewModel.Name,
                BankAccountType = bankAccountViewModel.BankAccountType,
                User = await GetCurrentUser()
            };

            if (ModelState.IsValid)
            {
                _context.Add(bankAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await GetBankAccount(id.Value);

            if (bankAccount == null)
            {
                return NotFound();
            }

            var bankAccountViewModel = new EditBankAccountViewModel
            {
                Id = bankAccount.Id,
                Name = bankAccount.Name,
                BankAccountType = bankAccount.BankAccountType,
            };

            return View(bankAccountViewModel);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,BankAccountType")] EditBankAccountViewModel bankAccountViewModel)
        {
            if (id != bankAccountViewModel.Id)
            {
                return NotFound();
            }

            var bankAccount = new BankAccount
            {
                Id = bankAccountViewModel.Id,
                Name = bankAccountViewModel.Name,
                BankAccountType = bankAccountViewModel.BankAccountType,
                User = await GetCurrentUser()
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountExists(bankAccount.Id))
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
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await GetBankAccount(id.Value);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var bankAccount = await GetBankAccount(id);
            _context.BankAccounts.Remove(bankAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountExists(long id)
        {
            return _context.BankAccounts.Include(b => b.User).Where(ba => ba.UserId == _currentUserId).Any(e => e.Id == id);
        }

        private async Task<BankAccount> GetBankAccount(long id)
        {
            return await _context.BankAccounts.Include(b => b.User)
                                              .Where(ba => ba.UserId == _currentUserId)
                                              .SingleOrDefaultAsync(m => m.Id == id);
        }
        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(User);
        }
    }
}
