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
        public static void ReplyWithGreeting(IBotContext context)
        {
            context.Batch().Reply($"Hello, I'm the multi-bot.");
        }

        public static void ReplyWithHelp(IBotContext context)
        {
            context.Batch().Reply($"I can demonstrate all different features of bot v4 ");
        }

        public static void ReplyWithResumeTopic(IBotContext context)
        {
            context.Batch().Reply($"What can I do for you?");
        }

        public static void ReplyWithConfused(IBotContext context)
        {
            context.Batch().Reply($"I am sorry, I didn't understand that.");
        }
    }
}
