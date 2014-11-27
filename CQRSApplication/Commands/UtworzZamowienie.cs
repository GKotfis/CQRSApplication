using System;

namespace CQRSApplication.Commands
{

    public class UtworzZamowienieCommand : ICommand
    {
        public string Kontrahent { get; set; }
        public DateTime Data { get; set; }
    }

    public class UtworzZamowienieHandler : IHandleCommand<UtworzZamowienieCommand>
    {

        public void Handle(UtworzZamowienieCommand command)
        {
            Console.WriteLine("Tworzenie zamówienia Kontrahent: {0}, Data: {1} ", command.Kontrahent, command.Data);
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("Dokument został utworzony ...");
        }
    }
}
