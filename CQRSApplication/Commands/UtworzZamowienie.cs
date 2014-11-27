using System;

namespace CQRSApplication.Commands
{
    /// <summary>
    /// ICommand definiuje tylko model komendy? Żadnej logiki?
    /// </summary>
    public class UtworzZamowienie : ICommand
    {
        public string Kontrahent { get; set; }
        public DateTime Data { get; set; }
    }

    public class UtworzZamowienieHandler : IHandleCommand<UtworzZamowienie>
    {

        public void Handle(UtworzZamowienie command)
        {
            Console.WriteLine("Tworzenie zamówienia Kontrahent: {0}, Data: {1} ", command.Kontrahent, command.Data);
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("Dokument został utworzony ...");
        }
    }
}
