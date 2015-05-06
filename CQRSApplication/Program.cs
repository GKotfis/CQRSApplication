using Autofac;
using CQRSApplication.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CQRSApplication
{
    internal class Program
    {
        private static IContainer _container;
        private static ICommandBus _commandBus;

        private static void Main(string[] args)
        {
            ConfigureIoC();

            _commandBus = _container.Resolve<ICommandBus>();

            _commandBus.SendCommand<Commands.UtworzZamowienieCommand>(new UtworzZamowienieCommand { Kontrahent = "erterter", Data = DateTime.Now });
            _commandBus.SendCommand<Commands.UtworzKontrahentaCommand>(new UtworzKontrahentaCommand { Kontrahent = "Yuola", Data = DateTime.Now, Miasto = "Gdynia" });

            Console.ReadKey();
        }

        private static void ConfigureIoC()
        {
            var containerBuilder = new ContainerBuilder();

            // CommandsBus
            containerBuilder.RegisterType<Commands.CommandBus>()
                            .As<Commands.ICommandBus>();

            var assembly = Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IHandleCommand<>));

            containerBuilder.Register<Func<Type, IHandleCommand>>(c =>
                            {
                                var context = c.Resolve<IComponentContext>();

                                return commandType =>
                                {
                                    Type handlerType = typeof(IHandleCommand<>).MakeGenericType(commandType);
                                    return (IHandleCommand)context.Resolve(handlerType);
                                };
                            });

            // EventsBus
            containerBuilder.RegisterType<Events.EventsBus>().As<Events.IEventsBus>();
            containerBuilder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(Events.IHandleEvent<>));

            containerBuilder.Register<Func<Type, IEnumerable<Events.IHandleEvent>>>(c =>
                {
                    var context = c.Resolve<IComponentContext>();

                    return eventType =>
                        {
                            Type handlerType = typeof(Events.IHandleEvent<>).MakeGenericType(eventType);
                            Type handlersType = typeof(IEnumerable<>).MakeGenericType(handlerType);

                            var handlersCollection = context.Resolve(handlersType);

                            return (IEnumerable<Events.IHandleEvent>)handlersCollection;
                        };
                });

            _container = containerBuilder.Build();
        }
    }
}

;