using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueExerciseOOP
{ 
    internal class OptionsMenuConsole
    {
        public static void Show(IPriorityQueue priorityQueue, IRegularQueue regularQueue, ICustomersServed customersServed, ref int countPriority, ref bool endApp)
        {
            int option = MainMenuConsole.ChooseOption();

            switch (option)
            {
                case 1:
                    regularQueue.Add(new RegularCustomer(OptionsConsole.InputRegularPersonName()));
                    StandardMessages.RefreshingPage();
                    break;
                case 2:
                    priorityQueue.Add(new PriorityCustomer(OptionsConsole.InputPriorityPersonName()));
                    StandardMessages.RefreshingPage();
                    break;
                case 3:
                    if (priorityQueue.CountPeopleInLine() == 0 && regularQueue.CountPeopleInLine() == 0)
                    {
                        StandardMessages.RefreshingPage();
                        StandardMessages.NoPersonInLine();
                    }
                    else if (countPriority < 3 && priorityQueue.CountPeopleInLine() > 0)
                    {
                        var priorityPersonServed = priorityQueue.CallNextInLine();
                        customersServed.Add(priorityPersonServed);
                        countPriority++;
                        StandardMessages.RefreshingPage();
                    }
                    else
                    {
                        var regularPersonServed = regularQueue.CallNextInLine();
                        customersServed.Add(regularPersonServed);
                        countPriority = 0;
                        StandardMessages.RefreshingPage();
                    }
                    break;
                case 9:
                    StandardMessages.ExitApp();
                    endApp = true;
                    break;
                default:
                    StandardMessages.RefreshingPage();
                    StandardMessages.InvalidNumber();
                    break;
            }
        }
    }
}
