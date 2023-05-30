using System.ComponentModel.DataAnnotations.Schema;
using WebAppExam.Models.Base;

namespace WebAppExam.Models
{
    public class Portfolio :BaseEntity
    {
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
