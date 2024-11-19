using BankApp.Application.Services.CurrencyService;
using BankApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApp.WebUI.Controllers
{
    public class CurrencyController : Controller
    {
       // private readonly ICurrencyRepository _repo;
       private readonly ICurrencyService _menager;

        public CurrencyController(ICurrencyService menager)
        {
            _menager = menager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _menager.GetAllCurrencies();
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
        public async Task<IActionResult> Create(Currency currency)
        {
           
           var response= await _menager.CreateCurrency(currency);
            return RedirectToAction(nameof(Index));


            return View(response.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var responde= await _menager.DeletCurrency(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var curr = await _menager.GetCurrencyById(id);
            if (curr.Data == null || curr.IsSuccess == false)
            {
                return NotFound();
            }
            return View(curr.Data);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortName,FullName")] Currency currency)
        {
            
           var stat = _menager.UpdateCurrency(currency);
            if (!stat.Result.IsSuccess)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
            
          
        }
    }
}

