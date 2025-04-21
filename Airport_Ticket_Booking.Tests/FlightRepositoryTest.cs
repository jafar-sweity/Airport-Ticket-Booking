using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.Services;
using Moq;

namespace Airport_Ticket_Booking.Tests
{
    public class FlightRepositoryTest
    {
        private readonly Mock<IFileStorage> _mockFileStorage;
        private readonly FlightRepository _repository;
        private readonly string _expectedPath = @"C:\Users\asus\source\repos\Airport_Ticket_Booking\Airport_Ticket_Booking\flights.csv";

        public FlightRepositoryTest()
        {
            _mockFileStorage = new Mock<IFileStorage>();
            _repository = new FlightRepository(_mockFileStorage.Object);
        }

        [Fact]
        public void GetAllFlights_ShouldCallReadFromFileWithCorrectPath()
        {
            // Arrange
            var mockFlights = new List<Flight>
            {
                new() { FlightNumber = 1 }
            };
            _mockFileStorage
                .Setup(fs => fs.ReadFromFile<Flight>(_expectedPath))
                .Returns(mockFlights);

            // Act
            var result = _repository.GetAllFlights();

            // Assert
            Assert.Equal(mockFlights, result);
            _mockFileStorage.Verify(fs => fs.ReadFromFile<Flight>(_expectedPath), Times.Once);
        }

        [Fact]
        public void SaveFlight_ShouldCallWriteToFileWithCorrectPath()
        {
            // Arrange
            var flights = new List<Flight>
            {
                new() { FlightNumber = 456 }
            };

            // Act
            _repository.SaveFlights(flights);

            // Assert
            _mockFileStorage.Verify(fs =>
                fs.WriteToFile<Flight>(flights, _expectedPath), Times.Once);
        }
    }
}
