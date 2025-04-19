using Airport_Ticket_Booking.models;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IBookingService
    {
        public void BookFlight(Booking booking);
        public void CancelBooking(int bookingId);
        public void ModifyBooking(Booking modifiedBooking);
        public List<Booking> GetBookingsForPassenger(int passengerId);
    }
}
