using Airport_Ticket_Booking.models.Enums;
using Airport_Ticket_Booking.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IBookingService
    {
        public void BookFlight(Passenger passenger, Flight flight, FlightClass flightClass);
        public void CancelBooking(int bookingId);
        public void ModifyBooking(Booking modifiedBooking);
        public List<Booking> GetBookingsForPassenger(int passengerId);
    }
}
