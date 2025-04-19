using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models.Enums;
using Airport_Ticket_Booking.models;

namespace Airport_Ticket_Booking.Services
{
    public class PassengerService(IBookingService bookingService, IFlightRepository flightRepository) : IPassengerService
    {
        private readonly IBookingService _bookingService = bookingService;
        private readonly IFlightRepository _flightRepository = flightRepository;

        public void BookFlight(Passenger passenger, Flight flight, FlightClass flightClass)
        {
            var booking = new Booking
            {
                BookingID = GenerateBookingId(),
                Flight = flight.FlightNumber,
                PassengerId = passenger.ID,
                FlightClass = flightClass,
                Price = CalculatePrice(flight.BasePrice, flightClass)
            };

            _bookingService.BookFlight(booking);
        }

        public void CancelBooking(int bookingId)
        {
            _bookingService.CancelBooking(bookingId);
        }

        public void ModifyBooking(Booking newBooking)
        {
            _bookingService.ModifyBooking(newBooking);
        }

        public List<Flight> SearchAvailableFlights(string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport, FlightClass flightClass)
        {
            var flights = _flightRepository.GetAllFlights();
            return [.. flights
                .Where(f => f.DepartureCountry == departureCountry && f.DestinationCountry == destinationCountry &&
                            f.DepartureTime.Date == departureDate.Date && f.DepartureAirport == departureAirport &&
                            f.ArrivalAirport == arrivalAirport)];
        }

        public List<Booking> ViewPersonalBookings(int passengerId)
        {
            return _bookingService.GetBookingsForPassenger(passengerId);
        }

        public static decimal CalculatePrice(decimal basePrice, FlightClass flightClass)
        {
            switch (flightClass)
            {
                case FlightClass.Economy:
                    return basePrice;
                case FlightClass.Business:
                    return basePrice * 1.5m;
                case FlightClass.FirstClass:
                    return basePrice * 2.0m;
                default:
                    Console.WriteLine("Invalid flight class.");
                    return -1;
            }
        }

        private int GenerateBookingId()
        {
            return new Random().Next(1, 99999);
        }
    }
}
