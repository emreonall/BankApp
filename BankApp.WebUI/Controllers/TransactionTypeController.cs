using BankApp.Database.Repositories.ProcessTypeRepo;
using BankApp.Database.Repositories.TransactionTypeRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankApp.WebUI.Controllers
{
    public class TransactionTypeController : Controller
    {
        private readonly ITransactionTypeRepository _repo;
        private readonly IProcessTypeRepository _processTypeRepo;

        TransacitonTypeValidator validationRules = new TransacitonTypeValidator();

        public TransactionTypeController(ITransactionTypeRepository repo, IProcessTypeRepository processTypeRepo)
        {
            _repo = repo;
            _processTypeRepo = processTypeRepo;
        }

  
        public async Task<IActionResult> Index()
        {
            var model = await _repo.GetAllWithProcessTypeAsync();
            return View(model.Data);
        }
        public IActionResult Create()
        {
            PopulateSelectLists();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "ProcessTypeId")] TransactionType transactionType)
        {
            var validationResult = validationRules.Validate(transactionType);
            validationResult.AddToModelState(ModelState, null);
            if (!ModelState.IsValid)
            {
                PopulateSelectLists();
                ViewBag.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return View();
            }
            await _repo.AddAsync(transactionType);
            return RedirectToAction(nameof(Index));
        }
        private void PopulateSelectLists(int? selectedProcessType = null)
        {
            var processTypes = _processTypeRepo.GetAllAsync();

            ViewBag.ProcessTypes = processTypes.Result.Data.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString(),
                Selected = selectedProcessType == x.Id
            }).ToList();
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
    }
}
