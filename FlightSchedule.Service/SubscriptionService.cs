using FlightSchedule.DtoModels;
using FlightSchedule.EntityBase.Entity;
using FlightSchedule.Repositories;
using FlightSchedule.Service.Contracts;
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



        public List<SubscriptionDto> GetAll(int agencyId)
        {
            Expression<Func<Subscription, bool>> filterSubscription = w => w.AgencyId==agencyId;
            return _unitOfWork.SubscriptionRepository.Get(filterSubscription).Select(a=>new SubscriptionDto()
            {
                AgencyId = a.AgencyId,
                DestinationCityId = a.DestinationCityId,
                OriginCityId=a.OriginCityId
            }).ToList();
        }
    }
}
