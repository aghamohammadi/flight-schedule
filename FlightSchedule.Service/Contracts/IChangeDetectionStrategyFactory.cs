using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSchedule.Service.Contracts
{
    public interface IChangeDetectionStrategyFactory
    {
        IChangeDetectionStrategy CreateStrategy(string strategyType);
    }

}
