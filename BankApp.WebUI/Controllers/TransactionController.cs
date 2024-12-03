using AutoMapper;
using BankApp.Database.Repositories.BankRepo;
using BankApp.Database.Repositories.CompanyRepo;
using BankApp.Database.Repositories.CurrencyRepo;
using BankApp.Database.Repositories.TransactionRepo;
using BankApp.Database.Repositories.TransactionTypeRepo;
using Microsoft.AspNetCore.Mvc;

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
    }
}
