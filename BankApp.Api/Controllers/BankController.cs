using BankApp.Database.Repositories.BankRepo;
using BankApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : Controller
    {
        private readonly IBankRepository _repo;

        public BankController(IBankRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _repo.GetAllBalanceSummaryWithCurrencyAsync();
            return Ok(model);
        }
    }
}
