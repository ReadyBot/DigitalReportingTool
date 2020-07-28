using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSchemaApp
{
    public class HandlingData
    {
        public HandlingData()
        {
            solutionData = new List<Solutions>();   
        }
        public List<Solutions> solutionData { get; set; }     
    }
    
}
