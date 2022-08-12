using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_I_Revised
{
    public class President
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public President(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
