using System.Reflection;
using System.Threading.Tasks;
using event_bus.Extensions;
using EventBus.Commands;
using EventBus.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;

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

    public static void AddRabbitMq(this IServiceCollection service, IConfiguration configuration)
    {
      var options = new RabbitMqOptions();
      var section = configuration.GetSection("RawRabbit");
      section.Bind(options);

      var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions { ClientConfiguration = options });
      service.AddSingleton<IBusClient>(_ => client);
    }

    private static string GetQueueName<T>()
    {
      return $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
    }
  }
}