using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Collections.Generic;
using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.Services;

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
            var mockFlights = new List<Flight>
            {
                new () { FlightNumber = 1 }
            };

            _mockFileStorage
                .Setup(fs => fs.ReadFromFile<Flight>(_expectedPath))
                .Returns(mockFlights);

            var result = _repository.GetAllFlights();

            Assert.Equal(mockFlights, result);
            _mockFileStorage.Verify(fs => fs.ReadFromFile<Flight>(_expectedPath), Times.Once);
        }
    }
}
