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
            var bookings = _bookingRepository.GetAllBookings();
            bookings.Add(booking);

            _bookingRepository.SaveBookings(bookings);
        }

        public void CancelBooking(int bookingId)
        {
            var bookings = _bookingRepository.GetAllBookings();
            var booking = bookings.FirstOrDefault(b => b.BookingID == bookingId);

            if (booking == null) return;

            bookings.Remove(booking);
            _bookingRepository.SaveBookings(bookings);
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
            var bookings = _bookingRepository.GetAllBookings();
            var existingBooking = bookings.FirstOrDefault(b => b.BookingID == modifiedBooking.BookingID);

            if (existingBooking == null) return;

            bookings.Remove(existingBooking);
            bookings.Add(modifiedBooking);

            _bookingRepository.SaveBookings(bookings);
        }
    }
}
