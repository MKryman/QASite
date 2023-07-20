using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Homework_05_01.Data
{
    public class UserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user,string password)
        {
            var context = new QuestionAnswerContext(_connectionString);
            
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if(user == null)
            {
                return null;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (isValidPassword)
            {
                return user;
            }

            return null;
        }

        public User GetByEmail(string email)
        {
            var context = new QuestionAnswerContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int userId)
        {
            var context = new QuestionAnswerContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public bool IsEmailAvail(string email)
        {
            var context = new QuestionAnswerContext(_connectionString);
            return context.Users.All(u => u.Email != email);
        }
    }
}
