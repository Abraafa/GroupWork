using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Groupwork.Services;

namespace Groupwork.UI
{
    public class MainMenu
    {
        private readonly BookingService _bookingService;

        public MainMenu()
        {
            _bookingService = new BookingService();
        }

        public void Show()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("Hotel Booking")
                        .Color(Color.Blue)
                        .Centered());

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Välj en funktion:[/]")
                        .AddChoices("Se tillgängliga rum", "Boka ett rum", "Avsluta"));

                switch (choice)
                {
                    case "Se tillgängliga rum":
                        ShowAvailableRooms();
                        break;
                    case "Boka ett rum":
                        BookRoom();
                        break;
                    case "Avsluta":
                        return;
                }
            }
        }

        private void ShowAvailableRooms()
        {
            var rooms = _bookingService.GetAvailableRooms();
            var table = new Table().AddColumn("ID").AddColumn("Namn").AddColumn("Pris");

            foreach (var room in rooms)
            {
                table.AddRow(room.RoomId.ToString(), room.RoomNumber, room.RoomType.RoomTypeId, room.IsAvailable == true ? "[green]Ja[/]" : "[red]Nej[/]");
            }

            AnsiConsole.Write(table);
            AnsiConsole.Markup("\n[grey]Tryck på en knapp för att fortsätta...[/]");
            Console.ReadKey();
        }

        private void BookRoom()
        {
            var roomId = AnsiConsole.Ask<int>("Ange rums-ID: ");
            var customerId = AnsiConsole.Ask<int>("Ange kund-ID: ");

            _bookingService.BookRoom(roomId, customerId);
            Console.ReadKey();
        }
        private int ReadInt(string prompt)
        {
            while (true)
            {
                try
                {
                    return AnsiConsole.Ask<int>(prompt);
                }
                catch
                {
                    AnsiConsole.Markup("[red]Felaktig inmatning! Ange ett heltal.[/]\n");
                }
            }
        }

    }
}
