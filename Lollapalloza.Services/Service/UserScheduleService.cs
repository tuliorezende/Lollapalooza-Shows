using Lollapalooza.Services.Interface;
using Lollapalooza.Services.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lollapalooza.Services.Service
{
    public class UserScheduleService : IUserScheduleService
    {
        private LollapaloozaContext _dataBase;
        private IScheduleExtensionService _scheduleExtensionService;
        public UserScheduleService(LollapaloozaContext lollapaloozaContext, IScheduleExtensionService scheduleExtensionService)
        {
            _dataBase = lollapaloozaContext;
            _scheduleExtensionService = scheduleExtensionService;
        }

        /// <summary>
        /// Method to create a User Schedule Entry
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="showId"></param>
        public void CreateUserScheduleEntry(string userIdentifier, int showId, bool showRemember, int timeMinutesToAlert)
        {
            _dataBase.UserSchedule.Add(new UserSchedule
            {
                ShowId = showId,
                UserIdentifier = userIdentifier,
                ScheduledDate = ReturnBrazilianDatetime(),
                ShowRemember = showRemember,
                TimeMinutesToAlert = timeMinutesToAlert
            });

            _dataBase.SaveChanges();

            if (showRemember)
            {
                var show = _dataBase.Show.Where(x => x.ShowId == showId).First();
                _scheduleExtensionService.InsertUserAtDistributionListAsync(userIdentifier, show, timeMinutesToAlert);
            }
        }

        /// <summary>
        /// Get all shows with user flag
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <returns></returns>
        public List<ShowScheduleFlags> GetAllShowsWithUserScheduleFlags(string userIdentifier)
        {
            var flaggedShows = _dataBase.Show.GroupJoin(_dataBase.UserSchedule,
                show => show.ShowId,
                schedule => schedule.ShowId,
                (show, schedule) => new { show, Scheduled = schedule })
                .SelectMany(showsmarcados => showsmarcados.Scheduled.DefaultIfEmpty(),
                (showsmarcados, schedule) => new ShowScheduleFlags { Show = showsmarcados.show, Scheduled = schedule != null }).ToList();

            return flaggedShows;
        }

        /// <summary>
        /// Get all show that user marked
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <returns></returns>
        public List<Show> GetUserScheduleShows(string userIdentifier)
        {
            List<Show> showList = _dataBase.UserSchedule.Where(x => x.UserIdentifier == userIdentifier).Select(x => x.Show).OrderBy(x => x.ShowId).ToList();

            if (showList.Count == 0)
                throw new Exception($"The query with useridentifier: {userIdentifier} returned 0 elements");

            return showList;

        }

        /// <summary>
        /// Manage User Schedule Services
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="showRemember"></param>
        /// <param name="timeMinutesToAlert"></param>
        public void ManageUserSchedule(string userIdentifier, bool showRemember, int timeMinutesToAlert)
        {
            List<UserSchedule> userSchedule = _dataBase.UserSchedule
                .Include(x => x.Show)
                .Where(x => x.UserIdentifier == userIdentifier).ToList();

            List<UserSchedule> userScheduleCopy = new List<UserSchedule>();

            //Create a copy of the original list
            foreach (var item in userSchedule)
            {
                userScheduleCopy.Add(new UserSchedule
                {
                    ScheduledDate = item.ScheduledDate,
                    Show = item.Show,
                    ShowId = item.ShowId,
                    ShowRemember = item.ShowRemember,
                    TimeMinutesToAlert = item.TimeMinutesToAlert,
                    UserIdentifier = item.UserIdentifier
                });
            }

            foreach (var item in userSchedule)
            {
                item.ShowRemember = showRemember;
                item.TimeMinutesToAlert = timeMinutesToAlert;
            }

            _dataBase.UpdateRange(userSchedule);
            _dataBase.SaveChanges();

            _scheduleExtensionService.ManageUserDistributionListAsync(userIdentifier, userScheduleCopy, showRemember, timeMinutesToAlert);
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

            foreach (var item in userSchedule)
            {
                var show = _dataBase.Show.Where(x => x.ShowId == item.ShowId).First();
                _scheduleExtensionService.RemoveUserFromDistributionListAsync(userIdentifier, show, item.TimeMinutesToAlert);
            }
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

            var show = _dataBase.Show.Where(x => x.ShowId == showId).First();
            _scheduleExtensionService.RemoveUserFromDistributionListAsync(userIdentifier, show, userSchedule.TimeMinutesToAlert);
        }

        /// <summary>
        /// Create Brazilian DateTime
        /// </summary>
        /// <returns></returns>
        private DateTime ReturnBrazilianDatetime()
        {
            DateTime userScheduledDate = DateTime.Now.ToUniversalTime();
            TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(userScheduledDate, timeInfo);
        }
    }
}
