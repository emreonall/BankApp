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
            await _repo.CreateTransactionType(transactionType);
            return RedirectToAction(nameof(Index));
        }
        private void PopulateSelectLists(int? selectedProcessType = null)
        {
            var processTypes = _processTypeRepo.GetAllProcessTypeAsync();

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
            var response = await _repo.DeleteTransactionTypeAsync(id);
            if (!response.IsSuccess)
            {
                ViewBag.Errors = response.Errors;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {

            var response = await _repo.GetTransactionTypeByIdAsync(id);

            if (response.Data == null || response.IsSuccess == false)
            {
                ViewBag.Errors = response.Errors;
                return NotFound();
            }
            PopulateSelectLists(response.Data.ProcessTypeId);
            return View(response.Data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Name, ProcessTypeId")] TransactionType transactionType)
        {
            if (id != transactionType.Id)
            {
                return BadRequest();
            }

            var validationResult = validationRules.Validate(transactionType);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    ViewBag.Errors = error.ErrorMessage;
                }
                PopulateSelectLists(transactionType.ProcessTypeId);
                return View(transactionType);
            }

            var stat = _repo.UpdateTransactionType(id, transactionType);
            if (!stat.IsSuccess)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));


        }
    }
}
