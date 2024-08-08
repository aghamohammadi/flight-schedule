using FlightSchedule.Repositories;
using FlightSchedule.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSchedule.Service
{
    public class ChangeDetectionStrategyFactory : IChangeDetectionStrategyFactory
    {
        private readonly IFlightService _flightService;
        private readonly IRouteService _routeService;
        private readonly ISubscriptionService _subscriptionService;

        public ChangeDetectionStrategyFactory(
            IFlightService flightService,
            IRouteService routeService,
            ISubscriptionService subscriptionService)
        {
            _flightService = flightService;
            _routeService = routeService;
            _subscriptionService = subscriptionService;
        }

        public IChangeDetectionStrategy CreateStrategy(string strategyType)
        {
            switch (strategyType.ToLower())
            {
                case "strategy1":
                    return new DefaultChangeDetectionStrategy( _flightService, _routeService);
                default:
                    throw new ArgumentException("Unknown strategy type");
            }
        }
    }

}
