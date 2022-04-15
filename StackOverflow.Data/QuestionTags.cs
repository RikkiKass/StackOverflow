using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    public class QuestionTags
    {
        public int TagId { get; set; }
        public int QuestionId { get; set; }
        public Tag Tag { get; set; }
        public Question Question { get; set; }
    }
}
