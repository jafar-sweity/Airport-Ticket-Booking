using Airport_Ticket_Booking.interfaces;
using Airport_Ticket_Booking.models;

namespace Airport_Ticket_Booking.Services
{
    public class BookingManager : IBookingManager
    {
        private readonly IFlightRepository _flightRepository;

        public BookingManager(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public void ImportFlightsFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"CSV file not found at: {filePath}");

            var lines = File.ReadAllLines(filePath).Skip(1); // Skip header

            var flights = new List<Flight>();
            int successCount = 0;
            int failCount = 0;

            foreach (var line in lines)
            {
                var flight = TryParseFlightFromCsvLine(line);
                if (flight != null)
                {
                    flights.Add(flight);
                    successCount++;
                }
                else
                {
                    failCount++;
                }
            }

            _flightRepository.SaveFlight(flights);

            Console.WriteLine($"{successCount} rows imported successfully, {failCount} rows failed.");
        }

        private Flight? TryParseFlightFromCsvLine(string line)
        {
            var values = line.Split(',').Select(v => v.Trim()).ToArray();

            if (values.Length != 7)
                return null;

            if (
                int.TryParse(values[0], out int flightNumber) &&
                DateTime.TryParse(values[3], out DateTime departureTime) &&
                decimal.TryParse(values[6], out decimal basePrice)
            )
            {
                return new Flight
                {
                    FlightNumber = flightNumber,
                    DepartureCountry = values[1],
                    DestinationCountry = values[2],
                    DepartureTime = departureTime,
                    DepartureAirport = values[4],
                    ArrivalAirport = values[5],
                    BasePrice = basePrice
                };
            }

            return null;
        }


        public List<Flight> FilterBookings(
            decimal? price = null,
            string departureCountry = null,
            string destinationCountry = null,
            DateTime? departureDate = null,
            string departureAirport = null,
            string arrivalAirport = null
        )
        {
            var flights = _flightRepository.GetAllFlights();

            if (price.HasValue)
                flights = flights.Where(f => f.BasePrice == price.Value).ToList();

            if (!string.IsNullOrWhiteSpace(departureCountry))
                flights = flights.Where(f =>
                    f.DepartureCountry.Equals(departureCountry, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(destinationCountry))
                flights = flights.Where(f =>
                    f.DestinationCountry.Equals(destinationCountry, StringComparison.OrdinalIgnoreCase)).ToList();

            if (departureDate.HasValue)
                flights = flights.Where(f => f.DepartureTime.Date == departureDate.Value.Date).ToList();

            if (!string.IsNullOrWhiteSpace(departureAirport))
                flights = flights.Where(f =>
                    f.DepartureAirport.Equals(departureAirport, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(arrivalAirport))
                flights = flights.Where(f =>
                    f.ArrivalAirport.Equals(arrivalAirport, StringComparison.OrdinalIgnoreCase)).ToList();

            return flights;
        }
    }
}
