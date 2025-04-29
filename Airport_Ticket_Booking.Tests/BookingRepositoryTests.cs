using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.Services;
using Moq;

namespace Airport_Ticket_Booking.Tests
{
    public class BookingRepositoryTests
    {
        private readonly Mock<IFileStorage> _mockFileStorage;
        private readonly BookingRepository _bookingRepository;
        private readonly string _expectedFilePath = @"C:\Users\asus\source\repos\Airport_Ticket_Booking\Airport_Ticket_Booking\bookings.csv";

        public BookingRepositoryTests()
        {
            _mockFileStorage = new Mock<IFileStorage>();
            _bookingRepository = new BookingRepository(_mockFileStorage.Object);
        }

        [Fact]
        public void GetAllBookings_ShouldCall_ReadFromFile_WithCorrectPath()
        {
            // Arrange
            var expectedBookings = new List<Booking> { new() };
            _mockFileStorage.Setup(fs => fs.ReadFromFile<Booking>(_expectedFilePath)).Returns(expectedBookings);

            // Act
            var result = _bookingRepository.GetAllBookings();

            // Assert
            Assert.Equal(expectedBookings, result);
            _mockFileStorage.Verify(fs => fs.ReadFromFile<Booking>(_expectedFilePath), Times.Once);
        }

        [Fact]
        public void SaveBookings_ShouldCall_WriteToFile_WithCorrectData()
        {
            // Arrange
            var bookings = new List<Booking>
            {
                new() { BookingID = 1, PassengerId = 2 }
            };

            // Act
            _bookingRepository.SaveBookings(bookings);

            // Assert
            _mockFileStorage.Verify(fs => fs.WriteToFile(bookings, _expectedFilePath), Times.Once);
        }
    }
}
