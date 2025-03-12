using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IFileStorage _fileStorage;
        private readonly string _filePath = @"C:\Users\asus\source\repos\Airport_Ticket_Booking\Airport_Ticket_Booking\bookings.csv";
        public BookingRepository(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }
        public List<Booking> GetAllBookings() { 
           return _fileStorage.ReadFromFile<Booking>(_filePath);
        }
        public void SaveBookings(List<Booking> bookings)
        {
          _fileStorage.WriteToFile<Booking>(bookings, _filePath);
        }
    }
}
