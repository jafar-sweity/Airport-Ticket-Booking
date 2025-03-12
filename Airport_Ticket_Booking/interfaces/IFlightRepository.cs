using Airport_Ticket_Booking.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IFlightRepository
    {
        public List<Flight> GetAllFlights();
        public void saveFlight(List<Flight> flight);
    }
}
