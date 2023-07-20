using Homework_05_01.Data;
using Homework_05_01.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework_05_01.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new QuestionRepository(_connectionString);

            return View(repo.GetQuestions());
        }

        [Authorize]
        public IActionResult NewQuestion()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewQuestion(Question question, List<string> tags)
        {
            var repo = new QuestionRepository(_connectionString);
            
            repo.AddQuestion(question, tags);
            return Redirect("/");
        }

        [Authorize]
        public IActionResult ViewQuestion(int id)
        {
            var repo = new QuestionRepository(_connectionString);
            var repos = new UserRepository(_connectionString);

            var question = repo.GetQuestionById(id);
            
            var questionUser = repos.GetByEmail(User.Identity.Name);
 
            return View(new ViewQuestionViewModel
            {
                Question = question,
                QuestionUser = questionUser
            });
        }

        [HttpPost] [Authorize]
        public IActionResult AddAnswer(Answer answer)
        {
            var repo = new QuestionRepository(_connectionString);
            var repos = new UserRepository(_connectionString);
            var user = repos.GetByEmail(User.Identity.Name);
            answer.Name = user.Name;
            repo.AddAnswer(answer);
            return Redirect($"/home/viewquestion?id={answer.QuestionId}");

        }

    }
}