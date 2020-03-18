using System.Reflection;
using System.Threading.Tasks;
using EventBus.Commands;
using EventBus.Events;
using RawRabbit;

namespace EventBus.Extensions
{
  public static class Extensions
  {
    public static Task WithCommandHandler<TCommand>(this IBusClient busClient, ICommandHandler<TCommand> handler) where TCommand : ICommand
    {
      return busClient.SubscribeAsync<TCommand>(msg => handler.Handle(msg), ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(queue => queue.WithName(GetQueueName<TCommand>()))));
    }

    public static Task WithEventHandler<TEvent>(this IBusClient busClient, IEventHandler<TEvent> handler) where TEvent : IEvent
    {
      return busClient.SubscribeAsync<TEvent>(msg => handler.Handle(msg), ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(queue => queue.WithName(GetQueueName<TEvent>()))));
    }

    private static string GetQueueName<T>()
    {
      return $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
    }
  }
}