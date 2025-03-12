using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.interfaces
{
    public interface IFileStorage
    {
        public void WriteToFile<T>(List<T>item, string filePath);
        public List<T> ReadFromFile<T>(string filePath);

    }
}
