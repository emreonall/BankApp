using BankApp.Database.Repositories.CurrencyRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.WebUI.Controllers
{
    public class CurrencyController : Controller
    {

        private readonly ICurrencyRepository _repo;
      
        public CurrencyController(ICurrencyRepository repo)
        {
            _repo = repo;

        }
        CurrencyValidator validator = new CurrencyValidator();
        public async Task<IActionResult> Index()
        {
            var model = await _repo.GetAllAsync();
           
            return View(model.Data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name","ShortName")] Currency currency)
        {
            CurrencyValidator validator = new CurrencyValidator();
            var validationResult = validator.Validate(currency);

            validationResult.AddToModelState(ModelState, null);


            if (!ModelState.IsValid)
            {
                ViewBag.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return View();
            }

            await _repo.AddAsync(currency);
            return RedirectToAction(nameof(Index));

            
           

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            var response = await _repo.Delete(id);
            if (!response.IsSuccess)
            {
                ViewBag.Errors = response.Errors;
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {

            var response = await _repo.GetByIdAsync(id);
            
            if (response.Data == null || response.IsSuccess == false)
            {
                ViewBag.Errors = response.Errors;
                return NotFound();
            }
            return View(response.Data);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortName,FullName")] Currency currency)
        {
            if (id != currency.Id)
            {
                return BadRequest();
            }

            var validationResult = validator.Validate(currency);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    ViewBag.Errors = error.ErrorMessage;
                }
                return View(currency);
            }

            var stat =  _repo.Update(id, currency);
            if (!stat.IsSuccess)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));


        }
    }
}

