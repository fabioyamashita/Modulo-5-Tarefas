using QueueExerciseOOP;
using System;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        IPriorityQueue priorityQueue = new PriorityQueue();
        IRegularQueue regularQueue = new RegularQueue();
        ICustomersServed customersServed = new CustomersServed();

        App.Init(priorityQueue, regularQueue, customersServed);
    }
}