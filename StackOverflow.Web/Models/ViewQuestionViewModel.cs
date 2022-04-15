using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackOverflow.Data;

namespace StackOverflow.Web.Models
{
    public class ViewQuestionViewModel
    {
        public Question Question { get; set; }
        public bool LikedItAlready { get; set; }
    }
}
