using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lollapalooza.Services.Interface;
using Lollapalooza.Services.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lollapalooza.Api.Controllers
{
    /// <summary>
    /// Validate User Schedule Entries
    /// </summary>
    [Produces("application/json")]
    [Route("api/UserSchedule")]
    public class UserScheduleController : Controller
    {
        private readonly IUserScheduleService _userSchedulerService;
        private readonly ICarouselService _carouselService;
        /// <summary>
        /// User Schedule Constructor
        /// </summary>
        /// <param name="userScheduleService"></param>
        /// <param name="carouselService"></param>
        public UserScheduleController(IUserScheduleService userScheduleService, ICarouselService carouselService)
        {
            _userSchedulerService = userScheduleService;
            _carouselService = carouselService;
        }

        /// <summary>
        /// Create a User Schedule Entry
        /// </summary>
        /// <param name="userIdentifier">BLiP user Identifier</param>
        /// <param name="showId">Show ID</param>
        /// <returns></returns>
        [HttpPost, Route("CreateUserSchedule/{userIdentifier}/{showId:int}")]
        public IActionResult Post(string userIdentifier, int showId = 0)
        {
            _userSchedulerService.CreateUserScheduleEntry(userIdentifier, showId);
            return Ok();
        }

        /// <summary>
        /// Remove User Schedule Entries. If don't pass show Id, this method will remove all entries for this user
        /// </summary>
        /// <param name="userIdentifier">BLiP user Identifier</param>
        /// <param name="showId">Show ID</param>
        /// <returns></returns>
        [HttpDelete, Route("RemoveUserSchedule/{userIdentifier}/{showId:int}")]
        public IActionResult Delete(string userIdentifier, int showId = 0)
        {
            if (showId != 0)
                _userSchedulerService.RemoveUserScheduleEntry(userIdentifier, showId);
            else
                _userSchedulerService.RemoveAllUserScheduleEntry(userIdentifier);

            return Ok();
        }


    }
}