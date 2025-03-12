using Airport_Ticket_Booking.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IBookingRepository
    {
        public void SaveBookings(List<Booking> bookings);
        public List<Booking> GetAllBookings();
    }
}
