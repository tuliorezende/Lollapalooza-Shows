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
        /// <summary>
        /// Create Carousel With All Shows for a specific stage and day
        /// </summary>
        /// <param name="shows"></param>
        /// <returns></returns>
        public DocumentCollection CreateCarouselWithAllShows(List<Show> shows)
        {
            List<DocumentSelect> documentSelect = new List<DocumentSelect>();
            foreach (Show item in shows)
            {
                documentSelect.Add(new DocumentSelect
                {
                    Header = new DocumentContainer
                    {
                        Value = CreateMediaLinkShow(item)
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

            return CreateDocumentCollection(documentSelect);
        }
        /// <summary>
        /// Create Carousel with user marked shows
        /// </summary>
        /// <param name="shows"></param>
        /// <returns></returns>
        public DocumentCollection CreateCarouselWithMarkedShows(List<Show> shows)
        {
            List<DocumentSelect> documentSelect = new List<DocumentSelect>();
            foreach (Show item in shows)
            {
                documentSelect.Add(new DocumentSelect
                {
                    Header = new DocumentContainer
                    {
                        Value = CreateMediaLinkShow(item)
                    },
                    Options = new DocumentSelectOption[]
                {
                    new DocumentSelectOption
                    {
                        Value = new DocumentContainer
                        {
                            Value = new PlainText
                            {
                                Text = "Teste Agendado"
                            }
                        },
                        Label = new DocumentContainer
                        {
                            Value = new PlainText
                            {
                                Text = "Teste Agendado"
                            }
                        }
                    }
                }
                });
            }

            return CreateDocumentCollection(documentSelect);
        }

        /// <summary>
        /// Creae Document Collection
        /// </summary>
        /// <param name="documentSelect"></param>
        /// <returns></returns>
        private DocumentCollection CreateDocumentCollection(List<DocumentSelect> documentSelect)
        {
            return new DocumentCollection
            {
                ItemType = DocumentSelect.MediaType,
                Items = documentSelect.ToArray()
            };
        }

        /// <summary>
        /// Create Media Link element with shows informations
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        private MediaLink CreateMediaLinkShow(Show show)
        {
            return new MediaLink
            {
                Uri = new Uri(show.ImageUrl),
                Title = $"{show.Stage} - {show.Band}",
                Text = $"{show.StartTime} - {show.EndTime}"
            };
        }

    }
}
