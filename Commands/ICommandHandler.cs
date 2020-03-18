using System.Threading.Tasks;

namespace EventBus.Commands
{
  public interface ICommandHandler<in T> where T : ICommand
  {
    Task Handle(T command);
  }
}