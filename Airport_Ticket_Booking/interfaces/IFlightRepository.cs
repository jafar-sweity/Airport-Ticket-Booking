using Airport_Ticket_Booking.models;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IFlightRepository
    {
        public List<Flight> GetAllFlights();
        public void SaveFlights(List<Flight> flight);
    }
}
