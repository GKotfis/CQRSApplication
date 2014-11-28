using Autofac;
using CQRSApplication.Commands;
using System;
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

            
            _container = containerBuilder.Build();

        }
    }
}
