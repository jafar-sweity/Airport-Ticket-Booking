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

        [Fact]
        public void CancelBooking_ShouldRemoveBookingAndSave()
        {
            var booking = new Booking { BookingID = 1 };
            var bookings = new List<Booking> { booking };

            _mockRepo.Setup(r => r.GetAllBookings()).Returns(bookings);

            _service.CancelBooking(1);

            _mockRepo.Verify(r => r.SaveBookings(It.Is<List<Booking>>(list => !list.Contains(booking))), Times.Once);
        }

        [Fact]
        public void CancelBooking_WithInvalidId_DoesNothing()
        {
            var bookings = new List<Booking>
            {
                new() { BookingID = 1 }
            };

            _mockRepo.Setup(r => r.GetAllBookings()).Returns(bookings);

            _service.CancelBooking(999);

            _mockRepo.Verify(r => r.SaveBookings(It.IsAny<List<Booking>>()), Times.Never);
        }

        [Fact]
        public void GetBookingsForPassenger_ShouldReturnCorrectBookings()
        {
            var bookings = new List<Booking>
            {
                new() { BookingID = 1, PassengerId = 1 },
                new() { BookingID = 2, PassengerId = 2 }
            };

            _mockRepo.Setup(r => r.GetAllBookings()).Returns(bookings);

            var result = _service.GetBookingsForPassenger(1);

            Assert.Single(result);
            Assert.Equal(1, result[0].PassengerId);
        }
    }
}