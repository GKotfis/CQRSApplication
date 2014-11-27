
namespace CQRSApplication.Commands
{
    public interface ICommandBus
    {
        void SendCommand<T>(T cmd) where T : ICommand;
    }
}
