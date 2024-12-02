using AutoMapper;
using BankApp.Database.Repositories.BankRepo;
using BankApp.Database.Repositories.CompanyRepo;
using BankApp.Database.Repositories.CurrencyRepo;
using BankApp.Database.Repositories.TransactionRepo;
using BankApp.Database.Repositories.TransactionTypeRepo;
using BankApp.Database.ViewModels;
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
            var banks = await _bankRepository.GetAllWithTransactionsAsync();

            List<BankListVM> model = _mapper.Map<List<BankListVM>>(banks.Data);
            foreach (var itemBank in model)
            {
                var balances = itemBank.Transactions.Where(b => b.Id == itemBank.Id).GroupBy(g => g.CurrencyId).Select(group=>group.Sum(transaction=>transaction.Amount));
            }

            return View(model);
        }
    }
}
