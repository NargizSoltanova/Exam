using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppExam.DAL;
using WebAppExam.Models;

namespace WebAppExam.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }
        public async Task<IActionResult> Edit(int id)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (setting == null) return NotFound();
            return View(setting);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Setting setting)
        {
            if (!ModelState.IsValid) return View();
            var exists = await _context.Settings.FirstOrDefaultAsync(x => x.Id == setting.Id);
            if (exists == null) return NotFound();
            exists.Key = setting.Key;
            exists.Value = setting.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
