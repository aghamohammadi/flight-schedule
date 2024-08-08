using FlightSchedule.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlightSchedule.DtoModels.Enums;

namespace FlightSchedule.Service.Contracts
{
    public interface IChangeDetectionStrategy
    {
        Task<List<FlightOutputResultDto>> DetectChanges(DateTime startDate, DateTime endDate, int agencyId);
    }

}
