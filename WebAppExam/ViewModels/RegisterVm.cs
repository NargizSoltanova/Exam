using System.ComponentModel.DataAnnotations;

namespace WebAppExam.ViewModels
{
    public class RegisterVm
    {
        public string FullName { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(70), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(50), DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(50), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
