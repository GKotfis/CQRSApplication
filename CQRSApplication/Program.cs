using Autofac;
using CQRSApplication.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CQRSApplication
{
    class Program
    {
        static IContainer _container;
        static ICommandBus _commandBus;

        static void Main(string[] args)
        {

            ConfigureIoC();

            _commandBus = _container.Resolve<ICommandBus>();

            _commandBus.SendCommand<Commands.UtworzZamowienieCommand>(new UtworzZamowienieCommand { Kontrahent = "erterter", Data = DateTime.Now });
            _commandBus.SendCommand<Commands.UtworzKontrahentaCommand>(new UtworzKontrahentaCommand { Kontrahent = "Yuola", Data = DateTime.Now, Miasto = "Gdynia" });

            Console.ReadKey();
        }

        static void ConfigureIoC()
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

                    //ICollection<Events.IHandleEvent> handlers = new List<Events.IHandleEvent>();
                    
                    return eventType =>
                        {
                            Type handlerType = typeof(Events.IHandleEvent<>).MakeGenericType(eventType);
                            
                            return (IEnumerable<Events.IHandleEvent>)context.Resolve(handlerType);

                            
                        };
                });

            _container = containerBuilder.Build();

        }
    }
}
