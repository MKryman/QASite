﻿namespace Homework_05_01.Data
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }
    }
}