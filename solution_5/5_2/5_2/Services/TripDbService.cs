using _5_2.Interfaces;
using _5_2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5_2.Services
{
    public class TripDbService : ITripDbRepository
    {
        private TripDbContext _context;

        public TripDbService()
        {
            _context = new TripDbContext();
        }

        public async Task<IEnumerable<Trip>> GetTripsAsync()
        {
            var trips = await _context.Trips
                .Include(trip => trip.CountryTrips)
                .OrderByDescending(trip => trip.DateFrom)
                .ToListAsync();
            return trips;
        }

        public async Task AssignClientToTrip(int idTrip, AddTripClient clientTrip)
        {
            var tmpClient = await _context.Clients
                .Where(cli => cli.Pesel == clientTrip.Pesel)
                .FirstOrDefaultAsync();

            if (tmpClient == default)
            {
                _context.Clients.Add(new Client()
                {
                    FirstName = clientTrip.FirstName,
                    LastName = clientTrip.LastName,
                    Telephone = clientTrip.Telephone,
                    Email = clientTrip.Email,
                    Pesel = clientTrip.Pesel
                });
                _context.SaveChanges();
                tmpClient = await _context.Clients
                    .Where(cli => cli.Pesel == clientTrip.Pesel)
                    .FirstOrDefaultAsync();
            }
            else
            {
                var checkTmpClientAssigned = await _context.ClientTrips
                    .Where(cliTrip => cliTrip.IdClient == tmpClient.IdClient && cliTrip.IdTrip == idTrip)
                    .FirstOrDefaultAsync();
                if (checkTmpClientAssigned != default)
                {
                    throw new System.ArgumentException("Already assigned to this trip");
                }
            }

            var trip = await _context.Trips
                .Where(trip => trip.IdTrip == idTrip)
                .FirstOrDefaultAsync();
            if (trip == default)
            {
                throw new System.ArgumentException("Trip does not exist");
            }

            // zapis klienta na wycieczke
            tmpClient.ClientTrips.Add(new ClientTrip()
            {
                IdClientNavigation = tmpClient,
                IdTripNavigation = trip,
                PaymentDate = clientTrip.PaymentDate,
                RegisteredAt = System.DateTime.Now
            });
            _context.SaveChanges();
        }
    }
}
