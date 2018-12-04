using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Financing.Models;

namespace Financing.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly TransactionContext _context;

        public TransactionsController(TransactionContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(int? id)
        {
            int target_year = int.Parse(DateTime.Now.Year.ToString());
            int target_month = int.Parse(DateTime.Now.Month.ToString());

            if(id != null)
            {
                if ((id >= 201601)&&(id <= 999912))
                {
                    var id_year = id.ToString().Substring(0, 4);
                    var id_month = id.ToString().Substring(4, 2);

                    target_year = int.Parse(id_year);
                    target_month = int.Parse(id_month);
                }
                else
                {
                    target_year = int.Parse(DateTime.Now.Year.ToString());
                    target_month = int.Parse(DateTime.Now.Month.ToString());
                }
            }

            var target_transactoions = _context.Transaction.Where(y => y.TargetMonth.Year == target_year).Where(m => m.TargetMonth.Month == target_month);
            var in_sum = 0;
            var out_sum = 0;
            foreach(var sum in target_transactoions.Where(s => s.Type == "in"))
            {
                in_sum += sum.Price;
            }
            foreach (var sum in target_transactoions.Where(s => s.Type == "out"))
            {
                out_sum += sum.Price;
            }
            var total_sum = in_sum - out_sum;
            var next_month = 0;
            var next_year = 0;
            var previous_month = 0;
            var previous_year = 0;

            if (target_month == 12)
            {
                next_year = target_year + 1;
                next_month = 1;
            }
            else
            {
                next_year = target_year;
                next_month = target_month + 1;
            }

            if (target_month == 1)
            {
                previous_year = target_year - 1;
                previous_month = 12;
            }
            else
            {
                previous_year = target_year;
                previous_month = target_month - 1;
            }

            if (next_month <= 9)
            {
                ViewData["target_next"] = next_year.ToString() + "0" + next_month.ToString();
            }
            else
            {
                ViewData["target_next"] = next_year.ToString() + next_month.ToString();
            }

            if (previous_month <= 9)
            {
                ViewData["target_previous"] = previous_year.ToString() + "0" + previous_month.ToString();
            }
            else
            {
                ViewData["target_previous"] = previous_year.ToString() + previous_month.ToString();
            }
            ViewData["this_year"] = target_year;
            ViewData["this_month"] = target_month;
            ViewData["in_sum"] = in_sum;
            ViewData["out_sum"] = out_sum;
            ViewData["total_sum"] = total_sum;

            return View(await target_transactoions.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Transactions/Create
        public IActionResult CreateIn()
        {
            return View();
        }

        // GET: Transactions/Create
        public IActionResult CreateOut()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Type,Title,Price,TargetMonth,CreateDate,Status")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIn([Bind("ID,Type,Title,Price,TargetMonth,CreateDate,Status")] Transaction transaction, string TargetMonth)
        {
            transaction.TargetMonth = DateTime.Parse(TargetMonth);
            transaction.Type = "in";
            transaction.CreateDate = DateTime.Now;
            transaction.Status = "done";

            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOut([Bind("ID,Type,Title,Price,TargetMonth,CreateDate,Status")] Transaction transaction, string TargetMonth)
        {
            transaction.TargetMonth = DateTime.Parse(TargetMonth);
            transaction.Type = "out";
            transaction.CreateDate = DateTime.Now;
            transaction.Status = "done";

            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Type,Title,Price,TargetMonth,CreateDate,Status")] Transaction transaction)
        {
            if (id != transaction.ID)
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
                    if (!TransactionExists(transaction.ID))
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
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.ID == id);
        }
    }
}
