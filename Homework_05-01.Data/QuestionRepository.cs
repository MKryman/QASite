using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Homework_05_01.Data
{
    public class QuestionRepository
    {
        private string _connectionString;

        public QuestionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Question> GetQuestions()
        {
            var context = new QuestionAnswerContext(_connectionString);
            return context.Questions.Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag)
                    .Include(q => q.Answers)
                    .ToList();

        }

        public Question GetQuestionById(int id)
        {
            var context = new QuestionAnswerContext(_connectionString);
            return context.Questions.Include(a => a.Answers)
                .Include(qt => qt.QuestionsTags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefault(q => q.Id == id);
        }

        private Tag GetTag(string tagName)
        {
            var context = new QuestionAnswerContext(_connectionString);
            return context.Tags.FirstOrDefault(t => t.Name == tagName);
        }

        private int AddTag(string tagName)
        {
            var context = new QuestionAnswerContext(_connectionString);
            var tag = new Tag { Name = tagName };
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag.Id;
        }

        public void AddQuestion(Question question, List<string> tags)
        {
            var context = new QuestionAnswerContext(_connectionString);
            question.Date = DateTime.Now;
            context.Questions.Add(question);
            context.SaveChanges();

            foreach (var tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;
                if (t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }
                context.QuestionsTags.Add(new QuestionTag
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });

                context.SaveChanges();
            }
        }

        public void AddAnswer(Answer answer)
        {
            var context = new QuestionAnswerContext(_connectionString);
            answer.Date = DateTime.Now;
            context.Answers.Add(answer);
            context.SaveChanges();
        }
    }
}
