using AutoMapper;
using BankApp.Database.DTOs.BankDtos;
using BankApp.Database.Repositories.BankRepo;
using BankApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApp.WebUI.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankRepository _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BankController(IBankRepository repo, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _repo = repo;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<Bank> model = await _repo.Hepsi();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Bank bank, IFormFile iconFile)
        {
            if (ModelState.IsValid)
            {
                if (iconFile != null && iconFile.Length > 0)
                {
                    // Dosya yolu kaydetme işlemi
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder); // Klasör yoksa oluştur
                    string filePath = Path.Combine(uploadsFolder, iconFile.FileName);

                    // Dosyayı kaydet
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await iconFile.CopyToAsync(fileStream);
                    }

                    // Tam yolu IconUrl alanına atama
                    bank.IconUrl = filePath;
                }
                await _repo.AddAsync(bank);

                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var bank = await _repo.GetByIdAsync(id);
            if (bank == null)
            {
                return NotFound();
            }
            return View(bank);
        }

        // POST: Bank/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IconUrl")] Bank bank, IFormFile? iconFile)
        {
            if (id != bank.Id)
            {
                return BadRequest();
            }
            var existingBank = await _repo.GetByIdAsync(id);
            if (existingBank == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existingBank.Name = bank.Name;

                    // Eğer yeni bir dosya yüklenmişse, eski değeri güncelle
                    if (iconFile != null && iconFile.Length > 0)
                    {
                        // Dosya yolu ayarlama
                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploadsFolder);
                        string filePath = Path.Combine(uploadsFolder, iconFile.FileName);

                        // Dosyayı kaydetme
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await iconFile.CopyToAsync(fileStream);
                        }

                        // Tam yolu IconUrl alanına atama
                        existingBank.IconUrl = filePath;
                    }

                    await _repo.UpdateAsync(id,existingBank);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!_context.Banks.Any(e => e.Id == bank.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }
    }
}
