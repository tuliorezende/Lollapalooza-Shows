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
    /// Return the list of showson BLiP format
    /// </summary>
    [Produces("application/json")]
    [Route("api/BlipShow")]
    public class BlipShowController : Controller
    {
        private readonly ICarouselService _carouselService;
        private readonly IShowService _showService;

        /// <summary>
        /// Instantiate some elements
        /// </summary>
        /// <param name="carouselService"></param>
        /// <param name="showService"></param>
        public BlipShowController(ICarouselService carouselService, IShowService showService)
        {
            _carouselService = carouselService;
            _showService = showService;
        }
        /// <summary>
        /// Generate Document Collection for Shows
        /// </summary>
        /// <param name="stage">Stage Name parameter</param>
        /// <param name="day">Day parameter</param>
        /// <returns></returns>
        [HttpGet, Route("{stage}/{day}")]
        public IActionResult Get(string stage, string day)
        {
            try
            {
                var showList = _showService.GetShows(stage, day);
                return Ok(_carouselService.CreateCarousel(showList));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}