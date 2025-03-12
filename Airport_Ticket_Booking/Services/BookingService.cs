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
    class BookingService : IBookingService
    {
        public BookingService() { }
        public void BookFlight(Passenger passenger, Flight flight, FlightClass flightClass)
        {
            throw new NotImplementedException();
        }
        public void CancelBooking(int bookingId)
        {
            throw new NotImplementedException();
        }
        public List<Booking> GetBookingsForPassenger(int passengerId)
        {
            throw new NotImplementedException();
        }
        public void ModifyBooking(Booking modifiedBooking)
        {
            throw new NotImplementedException();
        }
    }
}
