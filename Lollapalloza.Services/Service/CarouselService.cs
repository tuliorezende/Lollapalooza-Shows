using Lime.Messaging.Contents;
using Lime.Protocol;
using Lollapalooza.Services.Interface;
using Lollapalooza.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lollapalooza.Services.Service
{
    public class CarouselService : ICarouselService
    {
        public DocumentSelect[] CreateCarousel(List<Show> shows)
        {
            List<DocumentSelect> documentSelect = new List<DocumentSelect>();
            foreach (Show item in shows)
            {
                documentSelect.Add(new DocumentSelect
                {
                    Header = new DocumentContainer
                    {
                        Value = new MediaLink
                        {
                            Uri = new Uri(item.ImageUrl),
                            Title = $"{item.Stage} - {item.Band}",
                            Text = $"{item.Schedule}"
                        }
                    },
                    Options = new DocumentSelectOption[]
                {
                    new DocumentSelectOption
                    {
                        Value = new DocumentContainer
                        {
                            Value = new PlainText
                            {
                                Text = "Teste"
                            }
                        },
                        Label = new DocumentContainer
                        {
                            Value = new PlainText
                            {
                                Text = "Teste"
                            }
                        }
                    }
                }
                });
            }
            return documentSelect.ToArray();
        }
    }
}
