using BankApp.Database.Repositories.CompanyRepo;
using BankApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BankApp.WebUI.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _repo;

        public CompanyController(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _repo.GetAllAsync();
            if (model.IsSuccess == false)
            {
                return NotFound();
            }
            return View(model.Data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddAsync(company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var comp = await _repo.GetByIdAsync(id);
            if (comp.Data == null || comp.IsSuccess==false)
            {
                return NotFound();
            }
            return View(comp.Data);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyCode,CompanyName")] Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }
            var existingCompany = await _repo.GetByIdAsync(id);
            if (existingCompany.Data == null || existingCompany.IsSuccess==false)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existingCompany.Data.CompanyName = company.CompanyName;
                    existingCompany.Data.CompanyCode = company.CompanyCode;
                    await _repo.Update(id, existingCompany.Data);

                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!_context.Banks.Any(e => e.Id == bank.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
    }
}
