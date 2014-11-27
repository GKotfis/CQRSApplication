using Autofac;
using CQRSApplication.Commands;
using System;
using System.Linq;

namespace CQRSApplication
{
    class Program
    {
        static IContainer container;
        static ICommandBus commandBus;

        static void Main(string[] args)
        {
            
            ConfigureIoC();
            commandBus = container.Resolve<ICommandBus>();

            //commandBus = new CommandBus(GetHandler_factory());
            
            commandBus.SendCommand<UtworzZamowienieCommand>(new UtworzZamowienieCommand { Kontrahent = "JanKowalski", Data = DateTime.Now });

            Console.ReadKey();
        }

        static void ConfigureIoC()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Commands.CommandBus>()
                            .As<Commands.ICommandBus>()
                            .InstancePerLifetimeScope();
                            

            containerBuilder.RegisterAssemblyTypes().AsClosedTypesOf(typeof(IHandleCommand<>));

            container = containerBuilder.Build();

        }

        private static Func<Type, IHandleCommand> GetHandler_factory()
        {
            var all_types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes());

            var command_handlers = all_types
                .Where(x => typeof(IHandleCommand<>).IsAssignableFrom(x));

            Func<Type, IHandleCommand> handler_factory = t =>
            {
                Type handler_type = command_handlers
                    .Single(x => x.GetGenericArguments()[0] == t);

                return (IHandleCommand)Activator.CreateInstance(handler_type);
            };

            return handler_factory;
        }
    }
}
