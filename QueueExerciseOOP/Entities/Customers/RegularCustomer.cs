using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{
    internal class RegularCustomer : IRegularCustomer
    {
        public string Name { get; set; }
        public CustomerType CustomerType { get; set; }

        public RegularCustomer(string name)
        {
            Name = name;
            CustomerType = CustomerType.Regular;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
