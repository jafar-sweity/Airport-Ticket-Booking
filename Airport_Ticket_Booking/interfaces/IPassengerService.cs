using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IPassengerService
    {
        public void BookFlight(Passenger passenger, Flight flight,FlightClass flightClass);
        public List<Flight> SearchAvailableFlights(string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport, FlightClass flightClass);
        public void CancelBooking(Passenger passenger, int bookingId);
        public void ModifyBooking(Passenger passenger, Booking newBooking);
        public List<Booking> ViewPersonalBookings(int passengerId);
    }
}
