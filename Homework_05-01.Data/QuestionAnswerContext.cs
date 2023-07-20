using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_05_01.Data
{
    public class QuestionAnswerContext : DbContext
    {
        private string _connectionString;

        public QuestionAnswerContext(string connnectionString)
        {
            _connectionString = connnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<QuestionTag>()
               .HasKey(qt => new { qt.QuestionId, qt.TagId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public  DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTag> QuestionsTags { get; set; }

    }
}
