namespace QueueExerciseOOP
{
    internal interface ICustomer
    {
        string Name { get; set; }
        CustomerType CustomerType { get; set; }
    }
}