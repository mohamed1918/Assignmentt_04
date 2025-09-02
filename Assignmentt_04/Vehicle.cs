#region TPC Vehicle
public abstract class Vehicle
{
    public int Id { get; set; }
    public string Model { get; set; }
}

public class Car : Vehicle
{
    public int Doors { get; set; }
}

public class Bike : Vehicle
{
    public bool HasGear { get; set; }
}
#endregion
