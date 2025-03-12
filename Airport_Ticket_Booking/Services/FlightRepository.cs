using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Services
{
    public class FlightRepository : IFlightRepository
    {
        private IFileStorage _fileStorage;
        private string _filePath = "flights.csv";
        public FlightRepository(IFileStorage fileStorage) {
            _fileStorage = fileStorage;
        }
        public List<Flight> GetAllFlights()
        {
            return _fileStorage.ReadFromFile<Flight>(_filePath);
        }
        public void saveFlight(List<Flight> flight)
        {
            _fileStorage.WriteToFile<Flight>(flight, _filePath);
        }

    }
}
