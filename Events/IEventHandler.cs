using System.Threading.Tasks;

namespace EventBus.Events
{
  public interface IEventHandler<in T> where T : IEvent
  {
    Task Handle(T @event);
  }
}