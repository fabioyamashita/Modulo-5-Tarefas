using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{
    internal class PriorityCustomer : IRegularCustomer
    {
        public string Name { get; set; }
        public CustomerType CustomerType { get; set; }

        public PriorityCustomer(string name)
        {
            Name = name;
            CustomerType = CustomerType.Priority;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
