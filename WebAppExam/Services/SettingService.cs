using WebAppExam.DAL;

namespace WebAppExam.Services
{
    public class SettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.ToList().ToDictionary(x=>x.Key,x=>x.Value);
        }
    }
}
