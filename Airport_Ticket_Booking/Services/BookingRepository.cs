using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using System.Collections.Generic;
using System.IO;

namespace Airport_Ticket_Booking.Services
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IFileStorage _fileStorage;
        private readonly string _filePath = @"C:\Users\asus\source\repos\Airport_Ticket_Booking\Airport_Ticket_Booking\bookings.csv";

        private List<Booking> _cachedBookings;

        public BookingRepository(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
            _cachedBookings = _fileStorage.ReadFromFile<Booking>(_filePath); // Initialize the cached list from the file
        }

        public List<Booking> GetAllBookings()
        {
            return _cachedBookings;
        }

        public void SaveBookings(List<Booking> bookings)
        {
            _fileStorage.WriteToFile<Booking>(bookings, _filePath);
        }

        public void Add(Booking booking)
        {
            _cachedBookings.Add(booking);
            SaveBookings(_cachedBookings); 
        }

        public void Remove(Booking booking)
        {
            _cachedBookings.Remove(booking);
            SaveBookings(_cachedBookings); 
        }
    }
}
