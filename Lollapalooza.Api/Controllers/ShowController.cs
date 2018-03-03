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
    [Produces("application/json")]
    [Route("api/Show")]

    public class ShowController : Controller
    {
        private readonly IShowService _showService;
        /// <summary>
        /// Show constructor
        /// </summary>
        /// <param name="database"></param>
        /// <param name="showService"></param>
        public ShowController(LollapaloozaContext database, IShowService showService)
        {
            _showService = showService;
        }
        /// <summary>
        /// Method to return the list of show of a specific day and a specific stage
        /// </summary>
        /// <param name="stage">Stage Name parameter</param>
        /// <param name="day">Day parameter</param>
        /// <returns></returns>
        [HttpGet, Route("{stage}/{day}")]
        public IActionResult Get(string stage, string day)
        {
            try
            {
                return Ok(_showService.GetShows(stage, day));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}