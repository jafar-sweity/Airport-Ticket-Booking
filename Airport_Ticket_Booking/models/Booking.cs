namespace Airport_Ticket_Booking.models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int Flight { get; set; }
        public int PassengerId { get; set; }
        public Enums.FlightClass FlightClass { get; set; }
        public decimal Price { get; set; }

    }
}
