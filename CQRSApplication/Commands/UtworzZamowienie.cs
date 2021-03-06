﻿using CQRSApplication.Events;
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
        private readonly IEventsBus _eventsBus;

        public UtworzZamowienieHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }


        public void Handle(UtworzZamowienieCommand command)
        {
            Console.WriteLine("Tworzenie zamówienia Kontrahent: {0}, Data: {1} ", command.Kontrahent, command.Data);
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("Dokument został utworzony. Publikowanie zdarzeń");

            _eventsBus.PublishEvent<ZamowienieUtworzone>(new ZamowienieUtworzone { GIDNumer = 1234, Symbol = "ZS-1234/2013" });
        }
    }
}
