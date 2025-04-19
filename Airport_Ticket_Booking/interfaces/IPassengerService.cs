using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.models.Enums;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IPassengerService
    {
        public void BookFlight(Passenger passenger, Flight flight,FlightClass flightClass);
        List<Flight> SearchAvailableFlights(string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport, FlightClass flightClass);
        public void CancelBooking(int bookingId);
        public void ModifyBooking(Booking newBooking);
        public List<Booking> ViewPersonalBookings(int passengerId);
    }
}
