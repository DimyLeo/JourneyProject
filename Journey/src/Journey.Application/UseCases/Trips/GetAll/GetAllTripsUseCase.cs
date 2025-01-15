using Journey.Communication.Responses;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.GetAll;

public class GetAllTripsUseCase
{
    public ResponseTripsJson Execute()
    {
        JourneyDbContext dbContext = new JourneyDbContext();

        List<Trip> trips = dbContext.Trips.ToList();

        return new ResponseTripsJson
        {
            Trips = trips.Select(trip => new ResponseShortTripJson 
            {
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
            }).ToList()
        };
    }
}
