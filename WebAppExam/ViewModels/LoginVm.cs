using System.ComponentModel.DataAnnotations;

namespace WebAppExam.ViewModels
{
    public class LoginVm
    {
        [MaxLength(70)]
        public string UserNameOrEmail { get; set; }
        [MaxLength(50), DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
