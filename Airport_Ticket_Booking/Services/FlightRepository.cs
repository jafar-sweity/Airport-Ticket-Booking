using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;

namespace Airport_Ticket_Booking.Services
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IFileStorage _fileStorage;
        private readonly string _filePath = @"C:\Users\asus\source\repos\Airport_Ticket_Booking\Airport_Ticket_Booking\flights.csv";

        public FlightRepository(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public List<Flight> GetAllFlights()
        {
            return _fileStorage.ReadFromFile<Flight>(_filePath);
        }

        public void SaveFlights(List<Flight> flights) 
        {
            _fileStorage.WriteToFile<Flight>(flights, _filePath);
        }
    }
}
