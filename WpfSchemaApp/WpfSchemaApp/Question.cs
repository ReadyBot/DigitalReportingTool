using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSchemaApp
{
    public class Question
    {    
        public Question() { AnswerData = new List<Answer>(); }
        public List<Answer> AnswerData { get; set; }
        public int QuestionID { get; set; }
        public String QuestionTxt { get; set; }
    }
}