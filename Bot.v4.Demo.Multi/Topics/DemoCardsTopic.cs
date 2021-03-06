﻿using Bot.v4.Demo.Multi.Models;
using Bot.v4.Demo.Multi.Responses;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.v4.Demo.Multi.Topics
{
    public class DemoCardsTopic : ITopic
    {
        public DemoCardsTopic()
        {

        }

        public string Name { get; set; } = "DemoCards";

        public bool Greeted { get; set; } = false;

        public Task<bool> StartTopic(MultiBotContext context)
        {
            switch (context.Activity.Type)
            {
                case ActivityTypes.Message:
                    // greet on first message if we haven't already 
                    if (!Greeted)
                    {
                        DemoCardsResponses.ReplyWithGreeting(context);
                        this.Greeted = true;
                    }
                    return this.ContinueTopic(context);
            }
            return Task.FromResult(true);
        }

        public Task<bool> ContinueTopic(MultiBotContext context)
        {
            // for messages
            if (context.Activity.Type == ActivityTypes.Message)
            {
                switch (context.RecognizedIntents.TopIntent?.Name)
                {

                    case "showCarousel":
                        DemoCardsResponses.ReplyWithCarousel(context);
                        return Task.FromResult(true);

                    case "showAttachment":
                        DemoCardsResponses.ReplyWithAttachment(context);
                        return Task.FromResult(true);

                    case "showHero":
                        DemoCardsResponses.ReplyWithHero(context);
                        return Task.FromResult(true);

                    case "help":
                        // show contextual help 
                        DemoCardsResponses.ReplyWithHelp(context);
                        return Task.FromResult(true);

                    case "mainMenu":
                        // prompt to go to main menu
                        // switch to the default topic
                        context.ConversationState.ActiveTopic = new DefaultTopic();
                        return context.ConversationState.ActiveTopic.StartTopic(context);

                    default:
                        // show our confusion
                        //DemoCardsResponses.ReplyWithConfused(context);
                        return Task.FromResult(true);
                }
            }

            return Task.FromResult(true);

        }

        public Task<bool> ResumeTopic(MultiBotContext context)
        {
            // just prompt the user to ask what they want to do
            DefaultResponses.ReplyWithResumeTopic(context);
            return Task.FromResult(true);
        }


    }
}
