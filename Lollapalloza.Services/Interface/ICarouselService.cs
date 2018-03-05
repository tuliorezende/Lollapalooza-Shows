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
        DocumentCollection CreateCarouselWithAllShows(List<Show> shows);
        DocumentCollection CreateCarouselWithMarkedShows(List<Show> shows);
    }
}
