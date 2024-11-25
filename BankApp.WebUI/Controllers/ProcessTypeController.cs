using BankApp.Database.Repositories.ProcessTypeRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private void PopulateSelectLists()
        {
            ViewBag.SymbolList =  new List<SelectListItem>
            {
                new SelectListItem {Text = "+", Value = "+"},
                new SelectListItem {Text = "-", Value = "-"}
            };
            ViewBag.MultiplierList = new List<SelectListItem>
            {
                new SelectListItem {Text = "1", Value = "1"},
                new SelectListItem {Text = "-1", Value = "-1"}
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
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
