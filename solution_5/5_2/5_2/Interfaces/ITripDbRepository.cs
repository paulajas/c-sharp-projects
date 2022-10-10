using _5_2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _5_2.Interfaces
{
    public interface ITripDbRepository
    {
        Task<IEnumerable<Trip>> GetTripsAsync();
        Task  AssignClientToTrip(int idTrip, AddTripClient clientTrip);
    }
}
