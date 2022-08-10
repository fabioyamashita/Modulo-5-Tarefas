using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{
    internal class App
    {
        public static void Init(IPriorityQueue priorityQueue, IRegularQueue regularQueue, ICustomersServed customersServed)
        {
            var countPriority = 0;

            var endApp = false;

            while (!endApp)
            {
                StandardLayouts.PriorityQueueSummaryTitle();
                QueueSummaryConsole.Print(priorityQueue);

                StandardLayouts.RegularQueueSummaryTitle();
                QueueSummaryConsole.Print(regularQueue);

                QueueSummaryConsole.Print(customersServed);

                MainMenuConsole.Show();

                OptionsMenuConsole.Show(priorityQueue, regularQueue, customersServed, ref countPriority, ref endApp);
            }
        }
    }
}
