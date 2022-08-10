using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{
    internal class CustomersServed : ICustomersServed
    {
        public List<ICustomer> Customers { get; set; }

        public CustomersServed()
        {
            Customers = new List<ICustomer>();
        }

        public void Add(ICustomer customer)
        {
            Customers.Add(customer);
        }

        public int Count()
        {
            return Customers.Count();
        }
    }
}
