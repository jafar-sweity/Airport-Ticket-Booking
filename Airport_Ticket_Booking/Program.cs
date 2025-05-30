﻿using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.Services;
using Airport_Ticket_Booking.Storage;
using Airport_Ticket_Booking.Utilities;

class Program
{
    static void Main(string[] args)
    {
        IFileStorage fileStorage = new FileStorage();
        IFlightRepository flightRepository = new FlightRepository(fileStorage);
        IBookingRepository bookingRepository = new BookingRepository(fileStorage);
        IBookingService bookingService = new BookingService(bookingRepository);
        IPassengerService passengerService = new PassengerService(bookingService, flightRepository);

        var menu = new Menu(bookingService, flightRepository, passengerService );
        menu.ShowMenu();
    }
}