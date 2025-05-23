﻿namespace Airport_Ticket_Booking.models
{
    public class Flight
    {
        public int FlightNumber { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureTime { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public decimal BasePrice { get; set; }
    }
}
