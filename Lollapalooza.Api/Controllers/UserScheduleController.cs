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
        public UserScheduleController(IUserScheduleService userScheduleService, ICarouselService carouselService)
        {
            _userSchedulerService = userScheduleService;
            _carouselService = carouselService;
        }

        /// <summary>
        /// Return User Schedule Entries
        /// </summary>
        /// <param name="userIdentifier">BLiP user Identifier</param>
        /// <param name="blipFormat">If this value is true, this method will serialize the list on Carousel Format, else, will return the original json</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string userIdentifier, bool blipFormat = true)
        {
            try
            {
                List<Show> showList = _userSchedulerService.GetUserScheduleShows(userIdentifier);

                if (blipFormat)
                    return Ok(_carouselService.CreateCarousel(showList));
                else
                    return Ok(showList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Create a User Schedule Entry
        /// </summary>
        /// <param name="userIdentifier">BLiP user Identifier</param>
        /// <param name="showId">Show ID</param>
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
        /// <param name="userIdentifier">BLiP user Identifier</param>
        /// <param name="showId">Show ID</param>
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