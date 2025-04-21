using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.Services;
using Moq;

namespace Airport_Ticket_Booking.Tests
{
    public class BookingManagerTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly BookingManager _bookingManager;

        public BookingManagerTests()
        {
            _flightRepositoryMock = new Mock<IFlightRepository>();
            _bookingManager = new BookingManager(_flightRepositoryMock.Object);
        }

        [Fact]
        public void FilterBookings_ByPrice_ReturnsMatchingFlights()
        {
            // Arrange
            var flights = new List<Flight>
            {
                new() { FlightNumber = 101, BasePrice = 100 },
                new() { FlightNumber = 102, BasePrice = 200 },
            };
            _flightRepositoryMock.Setup(r => r.GetAllFlights()).Returns(flights);

            // Act
            var result = _bookingManager.FilterBookings(price: 100);

            // Assert
            Assert.Single(result);
            Assert.Equal(101, result[0].FlightNumber);
        }

        [Fact]
        public void ImportFlightsFromCsv_ValidFile_CallsSaveFlightWithParsedFlights()
        {
            // Arrange
            var testCsvPath = "test_flights.csv";
            var csvContent = new[]
            {
                "FlightNumber,DepartureCountry,DestinationCountry,DepartureTime,DepartureAirport,ArrivalAirport,BasePrice",
                "101,USA,Germany,2025-05-01 10:00:00,JFK,FRA,350"
            };
            File.WriteAllLines(testCsvPath, csvContent);

            // Act
            _bookingManager.ImportFlightsFromCsv(testCsvPath);

            // Assert
            _flightRepositoryMock.Verify(r => r.SaveFlights(It.Is<List<Flight>>(f =>
                f.Count == 1 &&
                f[0].FlightNumber == 101 &&
                f[0].DepartureCountry == "USA" &&
                f[0].DestinationCountry == "Germany"
            )), Times.Once);

            File.Delete(testCsvPath);
        }

        [Fact]
        public void FilterBookings_ByDepartureCountry_ReturnsMatchingFlights()
        {
            // Arrange
            var flights = new List<Flight>
            {
                new() { FlightNumber = 101, DepartureCountry = "USA" },
                new() { FlightNumber = 102, DepartureCountry = "Germany" },
            };
            _flightRepositoryMock.Setup(r => r.GetAllFlights()).Returns(flights);

            // Act
            var result = _bookingManager.FilterBookings(departureCountry: "USA");

            // Assert
            Assert.Single(result);
            Assert.Equal(101, result[0].FlightNumber);
        }

        [Fact]
        public void ImportFlightsFromCsv_FileNotFound_ThrowsException()
        {
            // Arrange
            var invalidPath = "invalid.csv";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => _bookingManager.ImportFlightsFromCsv(invalidPath));
        }
    }
}
