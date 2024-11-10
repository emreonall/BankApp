using BankApp.Database.Repositories.BankRepo;
using BankApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.WebUI.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankRepository _repo;

        public BankController(IBankRepository repo)
        {
            _repo = repo;
        }

        public IActionResult  Index()
        {
            Bank b = new()
            {
                Name = "Akbank",
                IsActive = true
            };

            // _repo.AddAsync(b);

            //var model= _repo.GetAll();
            List<Bank> model = _repo.Hepsi();
            return View(model);
        }
    }
}
