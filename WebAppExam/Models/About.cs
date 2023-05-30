using WebAppExam.Models.Base;

namespace WebAppExam.Models
{
    public class About :BaseEntity
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
