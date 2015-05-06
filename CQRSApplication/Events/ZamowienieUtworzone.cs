using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApplication.Events
{
    public class ZamowienieUtworzone : IEvent
    {
        public int GIDNumer { get; set; }
        public string Symbol { get; set; }
    }

    public class WhenZamowienieUtworzone : IHandleEvent<ZamowienieUtworzone>
    {

        public void Handle(ZamowienieUtworzone @event)
        {
            Console.WriteLine("[Handler] Utworzono zamówienie. Id: {0}, Symbol: {1}", @event.GIDNumer, @event.Symbol);
        }
    }

    public class WhenZamowienieUtworzone_WyslijPotwierdzenie : IHandleEvent<ZamowienieUtworzone>
    {

        public void Handle(ZamowienieUtworzone @event)
        {
            Console.WriteLine("[Handler] Wysyłanie potwierdzenia mailem dla zamówienia Id: {0}, Symbol: {1}", @event.GIDNumer, @event.Symbol);
        }
    }
}
