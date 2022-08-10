using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{
    internal class RegularQueue : IRegularQueue
    {
        public Queue<ICustomer> Customers { get; set; }

        public RegularQueue()
        {
            Customers = new Queue<ICustomer>();
        }

        public void Add(ICustomer customer)
        {
            Customers.Enqueue(customer);
        }

        public ICustomer CallNextInLine()
        {
            var name = Customers.Dequeue();
            return name;
        }

        public ICustomer CheckNextInLine()
        {
            return Customers.Peek();
        }

        public int CountPeopleInLine()
        {
            return Customers.Count();
        }

    }
}
