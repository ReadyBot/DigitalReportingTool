using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSchemaApp
{
    public class ReportingData
    {
        public ReportingData()
        {
            QuestionData = new List <Question>();        
        }     
        public List <Question> QuestionData { get; set; }        
    }
}