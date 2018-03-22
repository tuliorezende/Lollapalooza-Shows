using Lollapalooza.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lollapalooza.Services.Interface
{
    public interface IUserScheduleService
    {
        void CreateUserScheduleEntry(string userIdentifier, int showId, bool showRemember, int timeMinutesToAlert);
        void RemoveUserScheduleEntry(string userIdentifier, int showId);
        void RemoveAllUserScheduleEntry(string userIdentifier);
        List<Show> GetUserScheduleShows(string userIdentifier);
        List<ShowScheduleFlags> GetAllShowsWithUserScheduleFlags(string userIdentifier);
        void ManageUserSchedule(string userIdentifier, bool showRemember, int timeMinutesToAlert);
    }
}
