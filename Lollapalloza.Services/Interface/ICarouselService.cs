using Lime.Messaging.Contents;
using Lime.Protocol;
using Lollapalooza.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lollapalooza.Services.Interface
{
    public interface ICarouselService
    {
        DocumentSelect[] CreateCarouselWithAllShows(List<Show> shows);
        DocumentSelect[] CreaeCarouselWithMarkedShows(List<Show> shows);
    }
}
