using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppExam.DAL;
using WebAppExam.Models;
using WebAppExam.Utilities;
using WebAppExam.ViewModels;

namespace WebAppExam.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PortfoliesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PortfoliesController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index(int page =1, int take = 2)
        {
            var portfolies = await _context.Portfolios.Skip((page-1)*take).Take(take).ToListAsync();
            PaginateVm<Portfolio> paginateVm = new PaginateVm<Portfolio>()
            {
                Items = portfolies,
                CurrentPage = page,
                Take = take,
                PageCount = PageCount(take)
            };
            return View(paginateVm);
        }
        private int PageCount(int take)
        {
            int count = _context.Portfolios.Count();
            return (int)Math.Ceiling((decimal)count / take);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Portfolio portfolio)
        {
            if (!ModelState.IsValid) return View();
            if(!CheckFile(portfolio.File, "image", 200000, out string message))
            {
                ModelState.AddModelError("File", message); return View();
            }
            portfolio.Image = await portfolio.File.SaveFileAsync(_environment.WebRootPath, "portfolio");
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.Id == id);
            if(portfolio == null) return NotFound();
            return View(portfolio);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Portfolio portfolio)
        {
            if (!ModelState.IsValid) return View();
            var exists = await _context.Portfolios.FirstOrDefaultAsync(x => x.Id == portfolio.Id);
            if (exists == null) return NotFound();
            if(portfolio.File != null)
            {
                if (!CheckFile(portfolio.File, "image", 200000, out string message))
                {
                    ModelState.AddModelError("File", message); return View();
                }
                exists.File.DeleteFile(_environment.WebRootPath, "portfolio", exists.Image);
                exists.Image = await portfolio.File.SaveFileAsync(_environment.WebRootPath, "portfolio");
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _context.Portfolios.FirstOrDefaultAsync(x => x.Id == id);
            if (exists == null) return NotFound();
            exists.File.DeleteFile(_environment.WebRootPath, "portfolio", exists.Image);
            _context.Portfolios.Remove(exists);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private bool CheckFile(IFormFile file, string type, int size, out string message)
        {
            message = string.Empty;
            if(!file.CheckFileType(type) || file.CheckFileSize(size))
            {
                message = $"File type must be {type} type and File size must be less than {size} kb";
                return false;
            }
            if (!file.CheckFileType(type))
            {
                message = $"File type must be {type} type";
                return false;
            }
            if (file.CheckFileSize(size))
            {
                message = $"File size must be less than {size} kb";
                return false;
            }
            return true;
        }
    }
}
