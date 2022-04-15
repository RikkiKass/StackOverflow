using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
        public List<QuestionTags> QuestionTags { get; set; }
        public List<Answer>Answers { get; set; }
        public List<Likes>Likes { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
   
    }
}
