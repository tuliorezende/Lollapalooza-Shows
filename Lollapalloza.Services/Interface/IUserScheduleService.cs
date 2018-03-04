using System;
using System.Collections.Generic;
using System.Text;

namespace Lollapalooza.Services.Interface
{
    public interface IUserScheduleService
    {
        void CreateUserScheduleEntry(string userIdentifier, int showId);
        void RemoveUserScheduleEntry(string userIdentifier, int showId);
        void RemoveAllUserScheduleEntry(string userIdentifier);
    }
}
