using Lollapalooza.Services.Interface;
using Lollapalooza.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lollapalooza.Services.Service
{
    public class UserScheduleService : IUserScheduleService
    {
        private LollapaloozaContext _dataBase;

        public UserScheduleService(LollapaloozaContext lollapaloozaContext)
        {
            _dataBase = lollapaloozaContext;
        }

        /// <summary>
        /// Method to create a User Schedule Entry
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="showId"></param>
        public void CreateUserScheduleEntry(string userIdentifier, int showId)
        {
            _dataBase.UserSchedule.Add(new UserSchedule
            {
                ShowId = showId,
                UserIdentifier = userIdentifier
            });

            _dataBase.SaveChanges();
        }

        /// <summary>
        /// Method to remove all User Schedule Entry
        /// </summary>
        /// <param name="userIdentifier"></param>
        public void RemoveAllUserScheduleEntry(string userIdentifier)
        {
            List<UserSchedule> userSchedule = _dataBase.UserSchedule.Where(x => x.UserIdentifier == userIdentifier).ToList();

            if (userSchedule == null)
                throw new Exception($"The delete operation with userIdentifier: {userIdentifier} returned 0 elements");

            _dataBase.UserSchedule.RemoveRange(userSchedule);
            _dataBase.SaveChanges();

        }

        /// <summary>
        /// Method to remove a User Schedule Entry
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="showId"></param>
        public void RemoveUserScheduleEntry(string userIdentifier, int showId)
        {
            UserSchedule userSchedule = _dataBase.UserSchedule.Where(x => x.UserIdentifier == userIdentifier && x.ShowId == showId).FirstOrDefault();

            if (userSchedule == null)
                throw new Exception($"The delete operation with userIdentifier: {userIdentifier} and showId: {showId} returned 0 elements");

            _dataBase.UserSchedule.Remove(userSchedule);
            _dataBase.SaveChanges();

        }
    }
}
