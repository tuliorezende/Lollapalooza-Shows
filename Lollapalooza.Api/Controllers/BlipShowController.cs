using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lollapalooza.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lollapalooza.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/BlipShow")]
    public class BlipShowController : Controller
    {
        private readonly ICarouselService _carouselService;
        private readonly IShowService _showService;

        public BlipShowController(ICarouselService carouselService, IShowService showService)
        {
            _carouselService = carouselService;
            _showService = showService;
        }
        /// <summary>
        /// Generate Document Collection for Shows
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public IActionResult Get(string stage, string day)
        {
            var showList = _showService.GetShows(stage, day);
            return Ok(_carouselService.CreateCarousel(showList));
        }
    }
}