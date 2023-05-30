using WebAppExam.Models.Base;

namespace WebAppExam.Models
{
    public class Hero :BaseEntity
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
    }
}
