using Lollapalooza.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lollapalooza.Services.Interface
{
    public interface IShowService
    {
        List<Show> GetShows(string stage, string day);
        List<Show> GetSpecificShows(string bandName, string stage, string day);
    }
}
