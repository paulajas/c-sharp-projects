using _5_2.Interfaces;
using _5_2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace _5_2.Services
{
    public class ClientDbService : IClientDbRepository
    {
        private TripDbContext _context = new TripDbContext();

        public async Task DeleteClient(int idClient)
        {
            var trips = await _context.ClientTrips
                .CountAsync(clientTrip => clientTrip.IdClient == idClient);
            if (trips != 0)
            {
                throw new ArgumentException("Operation not allowed: client assigned to trip");
            }
            var client = new Client { IdClient = idClient };
            _context.Clients.Attach(client);
            _context.Entry(client).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
