using System;

namespace CQRSApplication.Commands
{

    public class UtworzKontrahentaCommand : ICommand
    {
        public string Kontrahent { get; set; }
        public string Miasto { get; set; }
        public DateTime Data { get; set; }
    }

    public class UtworzKontrahentaHandler : IHandleCommand<UtworzKontrahentaCommand>
    {

        public void Handle(UtworzKontrahentaCommand command)
        {
            Console.WriteLine("Tworzenie Kontrahenta: {0}, Miasto: {1}, Data: {2} ", command.Kontrahent, command.Miasto, command.Data);
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("Kontrahent został utworzony ...");
        }
    }
}
