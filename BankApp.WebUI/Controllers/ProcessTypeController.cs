using BankApp.Database.Repositories.ProcessTypeRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BankApp.WebUI.Controllers
{
    public class ProcessTypeController : Controller
    {
        private readonly IProcessTypeRepository _repo;

        public ProcessTypeController(IProcessTypeRepository repo)
        {
            _repo = repo;
        }
        ProcessTypeValidator validationRules = new ProcessTypeValidator();

        public async Task<IActionResult> Index()
        {
            var model = await _repo.GetAllAsync();
            return View(model.Data);
        }
        public IActionResult Create()
        {
            PopulateSelectLists();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "Symbol", "Multiplier")] ProcessType processType)
        {
            var validationResult = validationRules.Validate(processType);
            validationResult.AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
            {
                PopulateSelectLists();
                ViewBag.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return View();
            }
            await _repo.AddAsync(processType);
            return RedirectToAction(nameof(Index));
        }
        private void PopulateSelectLists(string selectedSymbol = null, int? selectedMultiplier = null)
        {
            ViewBag.SymbolList = new List<SelectListItem>
            {
                new SelectListItem { Text = "+", Value = "+", Selected = selectedSymbol == "+" },
                new SelectListItem { Text = "-", Value = "-", Selected = selectedSymbol == "-" }
            };

            ViewBag.MultiplierList = new List<SelectListItem>
            {
                new SelectListItem { Text = "1", Value = "1", Selected = selectedMultiplier == 1 },
                new SelectListItem { Text = "-1", Value = "-1", Selected = selectedMultiplier == -1 }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _repo.Delete(id);
            if (!response.IsSuccess)
            {
                ViewBag.Errors = response.Errors;
                return RedirectToAction(nameof(Index));
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
            PopulateSelectLists(response.Data.Symbol, response.Data.Multiplier);
            return View(response.Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Name, Symbol, Multiplier")] ProcessType processType)
        {
            //if (id != processType.Id)
            //{
            //    return BadRequest();
            //}

            var validationResult = validationRules.Validate(processType);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    ViewBag.Errors = error.ErrorMessage;
                }
                PopulateSelectLists(processType.Symbol, processType.Multiplier);
                return View(processType);
            }

            var stat = _repo.Update(id, processType);
            if (!stat.IsSuccess)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));


        }
    }
}
