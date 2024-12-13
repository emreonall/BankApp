using AutoMapper;
using BankApp.Database.Repositories.BankRepo;
using BankApp.Database.Repositories.CompanyRepo;
using BankApp.Database.Repositories.CurrencyRepo;
using BankApp.Database.Repositories.TransactionRepo;
using BankApp.Database.Repositories.TransactionTypeRepo;
using BankApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace BankApp.WebUI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBankRepository _bankRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(IBankRepository bankRepository, ICompanyRepository companyRepository, ICurrencyRepository currencyRepository, ITransactionTypeRepository transactionTypeRepository, ITransactionRepository transactionRepository, IMapper mapper)
        {
            _bankRepository = bankRepository;
            _companyRepository = companyRepository;
            _currencyRepository = currencyRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> ChooseBank()
        {
            var model = await _bankRepository.GetAllBalanceSummaryWithCurrencyAsync();
            
            return View(model.Data);
        }
        public async Task<IActionResult> ListTransaction(int bankId)
        {
            DateOnly firstDay = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
            
            DateOnly lastDay = firstDay.AddMonths(1);
            ViewBag.BankId = bankId;

            var result = await _transactionRepository.GetAllWitRelationshipsAsync(t => t.BankId == bankId && t.CurrentDate >= firstDay && t.CurrentDate <= lastDay);

      
            List<Transaction> model = result.Data;
            if (model is not null)
            {
                ViewBag.bankName = model.FirstOrDefault()?.Bank?.Name;
                ViewBag.iconUrl = model.FirstOrDefault()?.Bank?.IconUrl;
            }
            else
            {
                model = new List<Transaction>();
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CreateTransaction(int bankId)
        {
            await PopulateSelectLists();
            ViewBag.BankId = bankId;

            return View();
        }

        private async Task PopulateSelectLists(int? selectedCompanyId = null, int? selectedTransactionType = null)
        {
            var companies = await _companyRepository.GetAllCompanyAsync();
            var transactionTypes = await _transactionTypeRepository.GetAllWithProcessTypeAsync();
            var currencies = await _currencyRepository.GetAllCurrencyAsync();

            ViewBag.Companies = companies.Data.Select(x => new SelectListItem
            {
                Text = x.CompanyName,
                Value = x.Id.ToString(),
                Selected = selectedCompanyId == x.Id
            }).ToList();

            ViewBag.TransactionTypes = transactionTypes.Data.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString(),
                Selected = selectedTransactionType == x.Id
            }).ToList();

            ViewBag.Currencies = currencies.Data.Select(x => new SelectListItem
            {
                Text = x.ShortName,
                Value = x.Id.ToString(),
                Selected = selectedTransactionType == x.Id
            }).ToList();
        }
    }
}
