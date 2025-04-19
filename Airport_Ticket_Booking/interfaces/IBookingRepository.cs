using Airport_Ticket_Booking.models;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IBookingRepository
    {
        public void SaveBookings(List<Booking> bookings);
        public List<Booking> GetAllBookings();
    }
}
