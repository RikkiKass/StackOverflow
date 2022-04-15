using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StackOverflow.Data
{
     public class StackOverflowRepository
    {
        private string _connectionString;
        public StackOverflowRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Signup(User user, string password)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            user.PasswordHash= BCrypt.Net.BCrypt.HashPassword(password);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User Login(string email, string password)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            var user= context.Users.FirstOrDefault(u =>u.Email == email);
            if (user == null)
            {
                return null;
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isValid ? user : null;

        }
        public User GetByEmail(string email)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void AddQuestionTags(Question question, List<string>tags)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            question.DatePosted = DateTime.Now;
            context.Questions.Add(question);
            context.SaveChanges();
            foreach(string name in tags)
            {
                Tag t= GetTag(name);
                int tagId;
                if (t == null)
                {
                    tagId = AddTag(name);
                }
                else
                {
                    tagId = t.Id;
                }
                context.QuestionTags.Add(new QuestionTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
                context.SaveChanges();

            }
        }
        private Tag GetTag(string tag)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            return context.Tags.FirstOrDefault(t => t.Name == tag);
        }
        private int AddTag(string name)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            var tag = new Tag
            {
                Name = name
            };
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag.Id;
        }
        public void UpdateLikes(int questionId, int userId)
        {

            using var context = new StackOverflowDataContext(_connectionString);
            if(context.Likes.Any(l => l.UserId == userId && l.QuestionId==questionId))
            {
                return;
            }
            
            context.Likes.Add(new Likes
            {
                UserId =userId,
                QuestionId = questionId
            });
            context.SaveChanges();
        }
        public List<Question> GetQuestions()
        {
            using var context = new StackOverflowDataContext(_connectionString);
            return context.Questions.Include(q => q.Likes)
                .Include(q => q.Answers).Include(q => q.QuestionTags).ThenInclude(qt => qt.Tag).ToList();

        }
        public Question GetQuestion(int id)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            var question = context.Questions.Include(q => q.Answers).ThenInclude(a=>a.User).Include(q => q.Likes).Include(q => q.User)
                .Include(q => q.QuestionTags).ThenInclude(qt => qt.Tag).FirstOrDefault(q => q.Id == id);
            return question;
             
        }
        public int GetLikes(int questionId)
        {
            using var context = new StackOverflowDataContext(_connectionString);
           return context.Likes.Where(l => l.QuestionId == questionId).ToList().Count;
          
        }
        public void AddAnswer(Answer answer)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            context.Answers.Add(answer);
            context.SaveChanges();
        }
    }
}
