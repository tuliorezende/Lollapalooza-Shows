using Lollapalooza.Services.Interface;
using Lollapalooza.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lollapalooza.Services.Service
{
    public class ShowService : IShowService
    {
        private LollapaloozaContext _dataBase;

        public ShowService(LollapaloozaContext lollapaloozaContext)
        {
            _dataBase = lollapaloozaContext;
        }

        public List<Show> GetShows(string stage, string day)
        {
            var showsList = _dataBase.Show.Where(x => x.Stage.ToLower() == stage.ToLower() && x.Day.ToLower() == day.ToLower()).ToList();

            if (showsList.Count == 0)
                throw new Exception($"The query with stage: {stage} and day: {day} returned 0 elements");

            return showsList;
        }

        public List<Show> GetSpecificShows(string bandName, string stage, string day)
        {
            throw new NotImplementedException();
        }
    }
}
