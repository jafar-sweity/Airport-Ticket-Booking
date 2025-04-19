using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models.Enums;
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
using System;
using System.Collections.Generic;
using Airport_Ticket_Booking.models;
using Airport_Ticket_Booking.models.Enums;
using Airport_Ticket_Booking.Services;
using Airport_Ticket_Booking.interfaces;

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
            var passenger = new Passenger { ID = 1 };
            var flight = new Flight { FlightNumber = 101, BasePrice = 200 };
            var flightClass = FlightClass.Business;

            _passengerService.BookFlight(passenger, flight, flightClass);

            _mockBookingService.Verify(bs => bs.BookFlight(It.Is<Booking>(
                b => b.PassengerId == passenger.ID &&
                     b.Flight == flight.FlightNumber &&
                     b.FlightClass == flightClass &&
                     b.Price == flight.BasePrice * 1.5m
            )), Times.Once);
        }


        [Fact]
        public void CancelBooking_ShouldCallBookingServiceCancel()
        {
            _passengerService.CancelBooking(5);

            _mockBookingService.Verify(bs => bs.CancelBooking(5), Times.Once);
        }

        [Fact]
        public void ModifyBooking_ShouldCallBookingServiceModify()
        {
            var booking = new Booking { BookingID = 99 };

            _passengerService.ModifyBooking(booking);

            _mockBookingService.Verify(bs => bs.ModifyBooking(booking), Times.Once);
        }
    }
}
