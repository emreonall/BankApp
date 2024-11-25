using BankApp.Database.Repositories.CompanyRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            CompanyValidator validator = new CompanyValidator();
            var validationResult = await validator.ValidateAsync(company);
            validationResult.AddToModelState(ModelState, null);

            if (ModelState.IsValid)
            {
                await _repo.AddAsync(company);
                return RedirectToAction(nameof(Index));
            }


            ViewBag.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
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
            if (comp.Data == null || comp.IsSuccess == false)
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
            if (existingCompany.Data == null || existingCompany.IsSuccess == false)
            {
                return NotFound();
            }

            CompanyValidator validator = new CompanyValidator();
            var validationResult = validator.Validate(company);
            validationResult.AddToModelState(ModelState, null);
           

            if (ModelState.IsValid)
            {
                try
                {
                    existingCompany.Data.CompanyName = company.CompanyName;
                    existingCompany.Data.CompanyCode = company.CompanyCode;
                    _repo.Update(id, existingCompany.Data);

                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return View(company);
        }
    }
}
