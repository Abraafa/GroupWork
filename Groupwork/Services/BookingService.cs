using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groupwork.Models;
using Microsoft.EntityFrameworkCore;

namespace Groupwork.Services
{
    public class BookingService
    {
        private readonly HotelBookingSystemContext _context;

        public BookingService()
        {
            _context = new HotelBookingSystemContext(); 
        }

        public List<Room> GetAvailableRooms()
        {
            return _context.Rooms.Include(r => r.RoomType).Where(r => r.IsAvailable == true).ToList();
        }

        public void BookRoom(int roomId, int customerId)
        {
            try
            {
                var room = _context.Rooms.Find(roomId);
                if (room == null)
                {
                    Console.WriteLine("[red]Rummet existerar inte.[/]");
                    return;
                }

                if (room.IsAvailable.HasValue && room.IsAvailable.Value)
                {
                    room.IsAvailable = false;
                    _context.SaveChanges();
                    Console.WriteLine("[green]Rummet är nu bokat![/]");
                }
                else
                {
                    Console.WriteLine("[red]Detta rum är inte tillgängligt.[/]");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(ex);
            }
        }

    }
}
