using Homework_05_01.Data;

namespace Homework_05_01.Web.Models
{
    public class ViewQuestionViewModel
    {
        public Question Question { get; set; }
        public User QuestionUser { get; set; }
        public List<Answer> Answers { get; set; }
       
    }
}
