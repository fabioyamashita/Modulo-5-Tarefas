
namespace QueueExerciseOOP
{
    internal interface IQueue
    {
        Queue<ICustomer> Customers { get; set; }

        void Add(ICustomer customer);
        ICustomer CallNextInLine();
        ICustomer CheckNextInLine();
        int CountPeopleInLine();
    }
}