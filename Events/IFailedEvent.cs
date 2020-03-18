namespace EventBus.Events
{
  public interface IFailedEvent : IEvent
  {
    string Reason { get; }
    string Code { get; }
  }
}