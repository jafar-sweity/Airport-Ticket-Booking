using Airport_Ticket_Booking.interfaces;

namespace Airport_Ticket_Booking.Storage
{
    class FileStorage : IFileStorage
    {
     public void WriteToFile<T>(List<T> item, string filePath){
            var json = System.Text.Json.JsonSerializer.Serialize(item);
            File.WriteAllText(filePath, json);
     }
     public List<T> ReadFromFile<T>(string filePath) {
            var json = File.ReadAllText(filePath);
            return System.Text.Json.JsonSerializer.Deserialize<List<T>>(json);
     }
    }
}
