using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.Services;
using Moq;
using Xunit;

namespace Airport_Ticket_Booking.Tests
{
    public class BookingRepositoryTests
    {
        private readonly Mock<IFileStorage> _mockFileStorage ;
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
            var expectedBookings = new List<Booking> { new() };
            _mockFileStorage.Setup(fs => fs.ReadFromFile<Booking>(_expectedFilePath)).Returns(expectedBookings);

            var result = _bookingRepository.GetAllBookings();

            Assert.Equal(expectedBookings, result);
            _mockFileStorage.Verify(fs => fs.ReadFromFile<Booking>(_expectedFilePath), Times.Once);
        }
    }
}
