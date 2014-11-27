using Autofac;
using System;

namespace CQRSApplication.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IComponentContext _context;

        public CommandBus(IComponentContext context)
        {
            _context = context;
        }

        public void SendCommand<T>(T cmd) where T : ICommand
        {
            // cross-cutting concerns
            // logowanie
            // autentykacja
            // walidacja
            // pomiar czasu
            // error handling

            Console.WriteLine(new string('-', 45));

            var handler = _context.Resolve<Commands.IHandleCommand<T>>();
            handler.Handle(cmd);
        }
    }
}
