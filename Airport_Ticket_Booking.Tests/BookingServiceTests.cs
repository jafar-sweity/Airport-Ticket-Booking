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
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _mockRepo;
        private readonly BookingService _service;

        public BookingServiceTests()
        {
            _mockRepo = new Mock<IBookingRepository>();
            _service = new BookingService(_mockRepo.Object);
        }

        [Fact]
        public void BookFlight_ShouldAddBookingAndSave()
        {
            var existing = new List<Booking>();
            var newBooking = new Booking { BookingID = 1 };

            _mockRepo.Setup(r => r.GetAllBookings()).Returns(existing);

            _service.BookFlight(newBooking);

            _mockRepo.Verify(r => r.SaveBookings(It.Is<List<Booking>>(list => list.Contains(newBooking))), Times.Once);
        }
    }
}