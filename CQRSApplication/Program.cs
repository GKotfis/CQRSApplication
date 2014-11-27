using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Bezpośrednie wywołanie handler'a
            Commands.UtworzZamowienieHandler commandHandler = new Commands.UtworzZamowienieHandler();
            commandHandler.Handle(new Commands.UtworzZamowienie { Kontrahent = "Test", Data = DateTime.Now});

           // Jak stworzyć instancję ICommandBus nie korzystając z IoC?
            Commands.ICommandBus commandBus = new Commands.CommandBus(null);
            commandBus.SendCommand(new Commands.UtworzZamowienie { Kontrahent = "JanKowalski", Data = DateTime.Now });

            Console.ReadKey();
        }
    }
}
