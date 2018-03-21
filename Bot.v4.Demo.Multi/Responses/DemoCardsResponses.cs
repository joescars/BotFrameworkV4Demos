using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.v4.Demo.Multi.Responses
{
    public static class DemoCardsResponses
    {
        private static string catUrl = "http://cdn3-www.cattime.com/assets/uploads/gallery/25-funny-cat-memes/01_FUNNY-CAT-MEME.jpg";

        public static void ReplyWithGreeting(ITurnContext context)
        {
            context.SendActivity($"Hello, Let's discover what you can do with cards.");
        }

        public static void ReplyWithHelp(ITurnContext context)
        {
            context.SendActivity($"I can demonstrate different card types.");

            context.SendActivity($"Try *hero card*, *attachment*, or *carousel* ");

            context.SendActivity($"Or to go back enter *main menu*");
        }

        public static void ReplyWithResumeTopic(ITurnContext context)
        {
            context.SendActivity($"What can I do for you?");
        }

        public static void ReplyWithConfused(ITurnContext context)
        {
            context.SendActivity($"I am sorry, I didn't understand that.");
        }

        public static void ReplyWithAttachment(ITurnContext context)
        {
            // Create the activity and add an attachment.
            var activity = MessageFactory.Attachment(
                new Attachment()
                {
                    ContentUrl = catUrl,
                    ContentType = "image/png"
                });

            // Send the activity as a reply to the user.
            context.SendActivity(activity);
        }

        public static void ReplyWithHero(ITurnContext context)
        {
            // Create the activity and attach a Hero card.
            var activity = MessageFactory.Attachment(
                new HeroCard(
                    title: "Fun Cat Meme",
                    images: new CardImage[] { new CardImage(url: catUrl) },
                    buttons: new CardAction[]
                    {
                 new CardAction(title: "meow", type: ActionTypes.ImBack, value: "meow")
                    })
                .ToAttachment());

            // Send the activity as a reply to the user.
            context.SendActivity(activity);
        }

        public static void ReplyWithCarousel(ITurnContext context)
        {
            // Create the activity and attach a set of Hero cards.
            var activity = MessageFactory.Carousel(
                new Attachment[]
                {
             new HeroCard(
                 title: "title1",
                 images: new CardImage[] { new CardImage(url: catUrl) },
                 buttons: new CardAction[]
                 {
                     new CardAction(title: "button1", type: ActionTypes.ImBack, value: "item1")
                 })
             .ToAttachment(),
             new HeroCard(
                 title: "title2",
                 images: new CardImage[] { new CardImage(url: catUrl) },
                 buttons: new CardAction[]
                 {
                     new CardAction(title: "button2", type: ActionTypes.ImBack, value: "item2")
                 })
             .ToAttachment(),
             new HeroCard(
                 title: "title3",
                 images: new CardImage[] { new CardImage(url: catUrl) },
                 buttons: new CardAction[]
                 {
                     new CardAction(title: "button3", type: ActionTypes.ImBack, value: "item3")
                 })
             .ToAttachment()
                });

            context.SendActivity(activity);
        }

    }
}
