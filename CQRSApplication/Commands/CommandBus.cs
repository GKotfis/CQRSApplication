using System;

namespace CQRSApplication.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, IHandleCommand> _handlersFactory;

        public CommandBus(Func<Type, IHandleCommand> handlersFactory)
        {
            _handlersFactory = handlersFactory;
        }

        public void SendCommand<T>(T cmd) where T : ICommand
        {
            // cross-cutting concerns
            // logowanie
            // autentykacja
            // walidacja
            // pomiar czasu
            // error handling

            var handler = (IHandleCommand<T>)_handlersFactory(typeof(T));
            handler.Handle(cmd);
        }
    }
}
