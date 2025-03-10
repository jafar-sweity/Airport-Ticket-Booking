using Airport_Ticket_Booking.models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.models
{
    class Booking
    {
        public int BookingID { get; set; }
        public int Flight { get; set; }
        public int PassengerId { get; set; }
        public Enums.FlightClass FlightClass { get; set; }
        public decimal Price { get; set; }

    }
}
