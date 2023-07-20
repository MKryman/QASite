namespace Homework_05_01.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public List<QuestionTag> QuestionsTags { get; set;}
        public List<Answer> Answers { get; set; }
    }
}