using Azure;
using BankApp.Application.Services.CurrencyService;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApp.WebUI.Controllers
{
    public class CurrencyController : Controller
    {
      
       private readonly ICurrencyService _manager;
        CurrencyValidator validator = new CurrencyValidator();
        public CurrencyController(ICurrencyService manager)
        {
            _manager = manager;

        }

        public async Task<IActionResult> Index()
        {
            var model = await _manager.GetAllAsync();
            if (model.IsSuccess == false)
            {
              //  return NotFound();
              ViewBag.Errors = model.Errors;
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
           
            var response= await _manager.AddAsync(currency,validator);
            if (!response.IsSuccess)
            {
                ViewBag.Errors = response.Errors;
                return View();
            }
            return RedirectToAction(nameof(Index));


       
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            
            var response= await _manager.DeleteAsync(id);
            if (!response.IsSuccess)
            {
                ViewBag.Errors = response.Errors;
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _manager.GetByIdAsync(id);
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
            
           var stat = await _manager.Update(id,currency,validator);
            if (!stat.IsSuccess)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
            
          
        }
    }
}

