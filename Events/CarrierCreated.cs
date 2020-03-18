namespace EventBus.Events
{
  public class CarrierCreated : IEvent
  {
    public string Name { get; set; }
    public string Email { get; set; }

    public CarrierCreated(string name, string email)
    {
      Name = name;
      Email = email;
    }
  }
}