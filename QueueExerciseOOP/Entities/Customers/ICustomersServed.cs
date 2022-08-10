
namespace QueueExerciseOOP
{
    internal interface ICustomersServed
    {
        List<ICustomer> Customers { get; set; }

        void Add(ICustomer customer);
        int Count();
    }
}