using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;

namespace Bot.v4.Demo.Multi.Responses
{
    public static class DefaultResponses
    {
        public static void ReplyWithGreeting(ITurnContext context)
        {
            context.Batch().Reply($"Hello, I'm the multi-bot.");
        }

        public static void ReplyWithHelp(ITurnContext context)
        {
            context.Batch().Reply($"I can demonstrate all different features of bot v4 ");
        }

        public static void ReplyWithResumeTopic(ITurnContext context)
        {
            context.Batch().Reply($"What can I do for you?");
        }

        public static void ReplyWithConfused(ITurnContext context)
        {
            context.Batch().Reply($"I am sorry, I didn't understand that.");
        }
    }
}
