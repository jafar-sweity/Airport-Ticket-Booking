using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Ticket_Booking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public void BookFlight(Booking booking)
        {
            _bookingRepository.Add(booking); 
        }

        public void CancelBooking(int bookingId)
        {
            var booking = _bookingRepository.GetAllBookings()
                .FirstOrDefault(b => b.BookingID == bookingId);

            if (booking == null) return; 

            _bookingRepository.Remove(booking); 
        }

        public List<Booking> GetBookingsForPassenger(int passengerId)
        {
            var bookings = _bookingRepository.GetAllBookings();

            return bookings
                .Where(b => b.PassengerId == passengerId)
                .ToList();
        }

        public void ModifyBooking(Booking modifiedBooking)
        {
            var existingBooking = _bookingRepository.GetAllBookings()
                .FirstOrDefault(b => b.BookingID == modifiedBooking.BookingID);

            if (existingBooking == null) return; 

            _bookingRepository.Remove(existingBooking); 
            _bookingRepository.Add(modifiedBooking);
        }
    }
}
