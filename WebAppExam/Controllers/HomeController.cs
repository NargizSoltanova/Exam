using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAppExam.DAL;
using WebAppExam.Models;
using WebAppExam.ViewModels;

namespace WebAppExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVm vm = new HomeVm()
            {
                Portfolios= await _context.Portfolios.Take(6).ToListAsync()
            };
            return View(vm);
        }
    }
}