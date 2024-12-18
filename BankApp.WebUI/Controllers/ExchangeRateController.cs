using BankApp.Database.Repositories.CurrencyRepo;
using BankApp.Database.Repositories.ExchangeRateRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankApp.WebUI.Controllers
{
    public class ExchangeRateController : Controller
    {
        private readonly IExchangeRateRepository _repo;
        private readonly ICurrencyRepository _currencyRepo;
        ExchangeRateValidator validationRules = new ExchangeRateValidator();

        public ExchangeRateController(IExchangeRateRepository repo, ICurrencyRepository currencyRepo)
        {
            _repo = repo;
            _currencyRepo = currencyRepo;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _repo.GetAllWithCurrencyAsync();
            return View(model.Data);
        }
        public IActionResult Create()
        {
            PopulateSelectLists();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "CurrentDate","Rate","CurrencyId")] ExchangeRate exchangeRate)
        {
            var validationResult = validationRules.Validate(exchangeRate);
            validationResult.AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
            {
                PopulateSelectLists();
                ViewBag.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return View();
            }
            await _repo.CreateExchRate(exchangeRate);
            return RedirectToAction(nameof(Index));
        }
        private void PopulateSelectLists(int? selectedCurrency = null)
        {
            var processTypes = _currencyRepo.GetAllCurrencyAsync();

            ViewBag.Currencies = processTypes.Result.Data.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString(),
                Selected = selectedCurrency == x.Id
            }).ToList();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _repo.DeleteExchRate(id);
            if (!response.IsSuccess)
            {
                ViewBag.Errors = response.Errors;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _repo.GetExchRateByIdAsync(id);

            if (response.Data == null || response.IsSuccess == false)
            {
                ViewBag.Errors = response.Errors;
                return NotFound();
            }
            PopulateSelectLists(response.Data.CurrencyId);
            return View(response.Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Name, CurrentDate, Rate, CurrencyId")] ExchangeRate exchangeRate)
        {
            if (id != exchangeRate.Id)
            {
                return BadRequest();
            }

            var validationResult = validationRules.Validate(exchangeRate);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    ViewBag.Errors = error.ErrorMessage;
                }
                PopulateSelectLists(exchangeRate.CurrencyId);
                return View(exchangeRate);
            }

            var stat = _repo.UpdateExchRate(id, exchangeRate);
            if (!stat.IsSuccess)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));


        }
    }
}
