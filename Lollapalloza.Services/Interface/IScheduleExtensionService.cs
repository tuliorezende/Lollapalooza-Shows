using Lollapalooza.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lollapalooza.Services.Interface
{
    public interface IScheduleExtensionService
    {
        Task InsertUserAtDistributionListAsync(string userIdentifier, Show show, int timeMinutesToAlert);
        Task RemoveUserFromDistributionListAsync(string userIdentifier, Show show, int timeMinutesToAlert);
        Task ManageUserDistributionListAsync(string userIdentifier, List<UserSchedule> oldUserSchedules, bool showRemember, int timeMinutesToAlert);
    }
}
