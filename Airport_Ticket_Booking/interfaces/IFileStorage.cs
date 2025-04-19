namespace Airport_Ticket_Booking.interfaces
{
    public interface IFileStorage
    {
        public void WriteToFile<T>(List<T>item, string filePath);
        public List<T> ReadFromFile<T>(string filePath);

    }
}
