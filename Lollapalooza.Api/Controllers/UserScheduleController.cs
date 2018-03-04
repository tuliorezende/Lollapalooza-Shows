using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lollapalooza.Services.Interface;
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

        /// <summary>
        /// User Schedule Constructor
        /// </summary>
        /// <param name="userScheduleService"></param>
        public UserScheduleController(IUserScheduleService userScheduleService)
        {
            _userSchedulerService = userScheduleService;
        }
        /// <summary>
        /// Create a User Schedule Entry
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="showId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(string userIdentifier, int showId)
        {
            try
            {
                _userSchedulerService.CreateUserScheduleEntry(userIdentifier, showId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Remove User Schedule Entries. If don't pass show Id, this method will remove all entries for this user
        /// </summary>
        /// <param name="userIdentifier"></param>
        /// <param name="showId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(string userIdentifier, int showId = 0)
        {
            try
            {
                if (showId != 0)
                    _userSchedulerService.RemoveUserScheduleEntry(userIdentifier, showId);
                else
                    _userSchedulerService.RemoveAllUserScheduleEntry(userIdentifier);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}