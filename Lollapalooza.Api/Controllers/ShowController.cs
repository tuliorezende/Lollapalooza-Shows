using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lollapalooza.Services.Model;
using Lollapalooza.Services.Interface;

namespace Lollapalooza.Api.Controllers
{
    /// <summary>
    /// Return the list of shows
    /// </summary>
    [Produces("application/json")]
    [Route("api/Show")]

    public class ShowController : Controller
    {
        private readonly IShowService _showService;
        private readonly IUserScheduleService _userSchedulerService;
        private readonly ICarouselService _carouselService;

        /// <summary>
        /// Show constructor
        /// </summary>
        /// <param name="showService"></param>
        /// <param name="userScheduleService"></param>
        /// <param name="carouselService"></param>

        public ShowController(IShowService showService, IUserScheduleService userScheduleService, ICarouselService carouselService)
        {
            _showService = showService;
            _userSchedulerService = userScheduleService;
            _carouselService = carouselService;
        }
        /// <summary>
        /// Method to return the list of show of a specific day and a specific stage
        /// </summary>
        /// <param name="stage">Stage Name parameter</param>
        /// <param name="day">Day parameter</param>
        /// <param name="blipFormat">If this value is true, this method will serialize the list on Carousel Format, else, will return the original json</param>
        /// <returns></returns>
        [HttpGet, Route("GetAllShows/{stage}/{day}/{blipFormat}")]
        public IActionResult Get(string stage, string day, bool blipFormat = true)
        {
            try
            {
                List<Show> showList = _showService.GetShows(stage, day);

                if (blipFormat)
                    return Ok(_carouselService.CreateCarouselWithAllShows(showList));
                else
                    return Ok(showList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Return Shows of a Specific user
        /// </summary>
        /// <param name="userIdentifier">BLiP user Identifier</param>
        /// <param name="blipFormat">If this value is true, this method will serialize the list on Carousel Format, else, will return the original json</param>
        /// <returns></returns>
        [HttpGet, Route("GetUserShows/{userIdentifier}/{blipFormat}")]
        public IActionResult Get(string userIdentifier, bool blipFormat = true)
        {
            try
            {
                List<Show> showList = _userSchedulerService.GetUserScheduleShows(userIdentifier);

                if (blipFormat)
                    return Ok(_carouselService.CreaeCarouselWithMarkedShows(showList));
                else
                    return Ok(showList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}