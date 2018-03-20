using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot.v4.Demo.Multi.Models;
using Bot.v4.Demo.Multi.Responses;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Bot.v4.Demo.Multi.Topics
{
    public class DefaultTopic : ITopic
    {
        public DefaultTopic() { }

        public string Name { get; set; } = "Default";

        public bool Greeted { get; set; } = false;

        public Task<bool> StartTopic(MultiBotContext context)
        {
            switch (context.Request.Type)
            {
                case ActivityTypes.ConversationUpdate:
                    {
                        // greet when added to conversation
                        foreach (var newMember in context.Request.MembersAdded)
                        {
                            if (newMember.Id != context.Request.Recipient.Id)
                            {
                                DefaultResponses.ReplyWithGreeting(context);
                                DefaultResponses.ReplyWithHelp(context);
                                DefaultResponses.ReplyWithResumeTopic(context);
                                this.Greeted = true;
                            }
                        }

                    }
                    break;

                case ActivityTypes.Message:
                    // greet on first message if we haven't already 
                    if (!Greeted)
                    {
                        DefaultResponses.ReplyWithGreeting(context);
                        this.Greeted = true;
                    }
                    return this.ContinueTopic(context);
            }
            return Task.FromResult(true);
        }

        public Task<bool> ContinueTopic(MultiBotContext context)
        {
            switch (context.Request.Type)
            {
                case ActivityTypes.Message:
                    switch (context.RecognizedIntents.TopIntent?.Name)
                    {
                        case "demoCards":
                            // switch to card demos
                            context.ConversationState.ActiveTopic = new DemoCardsTopic();
                            return context.ConversationState.ActiveTopic.StartTopic(context);

                        case "luisDemo":
                            // switch to LUIS Demos
                            context.ConversationState.ActiveTopic = new DemoLUISTopic();
                            return context.ConversationState.ActiveTopic.StartTopic(context);

                        case "help":
                            // show help
                            DefaultResponses.ReplyWithHelp(context);
                            return Task.FromResult(true);

                        default:
                            // show our confusion
                            DefaultResponses.ReplyWithConfused(context);
                            return Task.FromResult(true);
                    }

                default:
                    break;
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
