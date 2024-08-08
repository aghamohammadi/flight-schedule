using FlightSchedule.DtoModels;
using FlightSchedule.EntityBase.Entity;
using FlightSchedule.Repositories;
using FlightSchedule.Service.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlightSchedule.Service
{
    public class SubscriptionService : ISubscriptionService
    {

        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }



        public async Task<List<SubscriptionDto>> GetAllAsync(int agencyId)
        {
            Expression<Func<Subscription, bool>> filterSubscription = w => w.AgencyId==agencyId;
            return await _unitOfWork.SubscriptionRepository.Get(filterSubscription).Select(a=>new SubscriptionDto()
            {
                AgencyId = a.AgencyId,
                DestinationCityId = a.DestinationCityId,
                OriginCityId=a.OriginCityId
            }).ToListAsync();
        }
    }
}
