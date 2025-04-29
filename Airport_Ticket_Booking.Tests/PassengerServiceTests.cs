using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models.Enums;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.Services;
using Moq;
using Airport_Ticket_Booking.Constants;

namespace Airport_Ticket_Booking.Tests
{
    public class PassengerServiceTests
    {
        private readonly Mock<IBookingService> _mockBookingService;
        private readonly Mock<IFlightRepository> _mockFlightRepository;
        private readonly PassengerService _passengerService;

        public PassengerServiceTests()
        {
            _mockBookingService = new Mock<IBookingService>();
            _mockFlightRepository = new Mock<IFlightRepository>();
            _passengerService = new PassengerService(_mockBookingService.Object, _mockFlightRepository.Object);
        }

        [Fact]
        public void BookFlight_ShouldCallBookingServiceWithCorrectBooking()
        {
            // Arrange
            var passenger = new Passenger { ID = 1 };
            var flight = new Flight { FlightNumber = 101, BasePrice = 200 };
            var flightClass = FlightClass.Business;

            // Act
            _passengerService.BookFlight(passenger, flight, flightClass);

            // Assert
            _mockBookingService.Verify(bs => bs.BookFlight(It.Is<Booking>(
                b => b.PassengerId == passenger.ID &&
                     b.Flight == flight.FlightNumber &&
                     b.FlightClass == flightClass &&
                     b.Price == flight.BasePrice * FlightPricingConstants.BusinessMultiplier
            )), Times.Once);
        }

        [Fact]
        public void ModifyBooking_ShouldCallBookingServiceModify()
        {
            // Arrange
            var booking = new Booking { BookingID = 99 };

            // Act
            _passengerService.ModifyBooking(booking);

            // Assert
            _mockBookingService.Verify(bs => bs.ModifyBooking(booking), Times.Once);
        }

        [Fact]
        public void SearchAvailableFlights_ShouldReturnMatchingFlights()
        {
            // Arrange
            var targetDate = new DateTime(2025, 5, 1);
            var matchingFlight = new Flight
            {
                DepartureCountry = "USA",
                DestinationCountry = "Germany",
                DepartureTime = targetDate,
                DepartureAirport = "JFK",
                ArrivalAirport = "FRA"
            };
            var flights = new List<Flight> { matchingFlight };

            _mockFlightRepository
                .Setup(fr => fr.GetAllFlights())
                .Returns(flights);

            // Act
            var result = _passengerService.SearchAvailableFlights(
                matchingFlight.DepartureCountry,
                matchingFlight.DestinationCountry,
                matchingFlight.DepartureTime,
                matchingFlight.DepartureAirport,
                matchingFlight.ArrivalAirport,
                FlightClass.Economy);

            // Assert
            Assert.Single(result);
            Assert.Equal(matchingFlight.DestinationCountry, result[0].DestinationCountry);
        }


        [Fact]
        public void ViewPersonalBookings_ShouldReturnBookingsFromBookingService()
        {
            // Arrange
            var expectedBookings = new List<Booking>
            {
                new () { PassengerId = 1 }
            };
            _mockBookingService
                .Setup(bs => bs.GetBookingsForPassenger(1))
                .Returns(expectedBookings);

            // Act
            var result = _passengerService.ViewPersonalBookings(1);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].PassengerId);
        }

        [Theory]
        [InlineData(100, FlightClass.Economy, 100)]
        [InlineData(100, FlightClass.Business, 150)]
        [InlineData(100, FlightClass.FirstClass, 200)]
        public void CalculatePrice_ShouldReturnCorrectValue(decimal basePrice, FlightClass flightClass, decimal expected)
        {
            // Arrange (nothing to arrange here since method is static)

            // Act
            var result = PassengerService.CalculatePrice(basePrice, flightClass);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
