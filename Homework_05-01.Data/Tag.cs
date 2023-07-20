namespace Homework_05_01.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<QuestionTag> QuestionsTags { get; set; }
    }
}