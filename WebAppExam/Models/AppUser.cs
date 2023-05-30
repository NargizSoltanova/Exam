using Microsoft.AspNetCore.Identity;

namespace WebAppExam.Models
{
    public class AppUser :IdentityUser
    {
        public string FullName { get; set; }
    }
}
