using AutoMapper;
using BankApp.Database.DTOs.BankDtos;
using BankApp.Database.Repositories.BankRepo;
using BankApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.WebUI.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankRepository _repo;
        private readonly IMapper _mapper;

        public BankController(IBankRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            Bank banka = new()
            {
                Name = "Akbank",
                IconUrl = null,

            };

            //var bank = new Bank {Name = "Test Bank" ,IconUrl=null};
            //var dto = _mapper.Map<BankListDto>(bank);

            //  _repo.AddAsync(_mapper.Map<Bank>(banka));
         //   await _repo.AddAsync(banka);
            //var model= _repo.GetAll();

            // List<Bank> banks= await _repo.Hepsi();

            var singleBank = (await _repo.Hepsi()).FirstOrDefault();
            var model = _mapper.Map<BankListDto>(singleBank);

            try
            {
                List<BankListDto> models = _mapper.Map<List<BankListDto>>(singleBank);
            }
            catch (Exception ex)
            {

                throw;
            }
            
          //  List<Bank> model = await _repo.Hepsi();
            return View(model);
        }
    }
}
