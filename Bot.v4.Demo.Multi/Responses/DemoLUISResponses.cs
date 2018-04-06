using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.LUIS;

namespace Bot.v4.Demo.Multi.Responses
{
    public static class DemoLUISResonses
    {
        public static void ReplyWithGreeting(ITurnContext context)
        {
            context.SendActivity($"Hello, Let's Discover what you can do with LUIS!");
        }

        public static void ReplyWithHelp(ITurnContext context)
        {
            context.SendActivity($"Any text you enter will be sent to LUIS and we will return matching intents and entities.");
        }

        public static void ReplyWithResumeTopic(ITurnContext context)
        {
            context.SendActivity($"What can I do for you?");
        }

        public static void ReplyWithConfused(ITurnContext context)
        {
            context.SendActivity($"I am sorry, I didn't understand that.");
        }

        public static void ReplyWithLUISResult(ITurnContext context)
        {
            context.SendActivity("Sending to Luis...");

            // Get list of itents
            var results = context.Services.Get<RecognizerResult>(LuisRecognizerMiddleware.LuisRecognizerResultKey);

            // Get top Intent
            var topIntent = results.GetTopScoringIntent();

            switch (topIntent.key)
            {
                case null:
                case "None":
                    context.SendActivity("Apologies, I dont understand");
                    break;
                default:
                    context.SendActivity($"Itent: {topIntent.key}, Entities: {results.Entities.Count}");
                    break;
            }

        }
    }
}
