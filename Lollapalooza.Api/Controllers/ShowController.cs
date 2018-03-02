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
        private LollapaloozaContext _database;
        private readonly IShowService _showService;
        public ShowController(LollapaloozaContext database, IShowService showService)
        {
            _database = database;
            _showService = showService;
        }
        /// <summary>
        /// Method to return the list of show of a specific day and a specific stage
        /// </summary>
        /// <param name="stage">Stage Name</param>
        /// <param name="day"></param>
        /// <returns></returns>
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