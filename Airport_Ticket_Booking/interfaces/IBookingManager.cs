using Airport_Ticket_Booking.models;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IBookingManager
    {
        public void ImportFlightsFromCsv(string filePath);
        List<Flight> FilterBookings(
         decimal? price = null,
         string departureCountry = null,
         string destinationCountry = null,
         DateTime? departureDate = null,
         string departureAirport = null,
         string arrivalAirport = null
     );
    }
}
