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

        public async Task<IActionResult>  Index()
        {
            Bank banka = new()
            {
                Name = "Akbank",
                IconUrl = null,
                
            };

            //var bank = new Bank {Name = "Test Bank" ,IconUrl=null};
            //var dto = _mapper.Map<BankListDto>(bank);

          //  _repo.AddAsync(_mapper.Map<Bank>(banka));
        ///  _repo.AddAsync(banka);
            //var model= _repo.GetAll();

          //  List<BankListDto> model = _mapper.Map<List<BankListDto>>(_repo.Hepsi());
           List<Bank> model = await _repo.Hepsi();
            return View(model);
        }
    }
}
