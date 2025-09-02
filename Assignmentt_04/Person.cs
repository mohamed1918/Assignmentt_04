#region TPH Person
public abstract class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CustomerPerson : Person
{
    public string Address { get; set; }
}

public class EmployeePerson : Person
{
    public decimal Salary { get; set; }
}
#endregion

