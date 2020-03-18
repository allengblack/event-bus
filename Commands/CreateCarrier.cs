namespace EventBus.Commands
{
  public class CreateCarrier : ICommand
  {
    public string Name { get; set; }
    public string Email { get; set; }
  }
}