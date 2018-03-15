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
                        var activity = context.Request.AsConversationUpdateActivity();
                        if (IsNewMember(activity))
                        {
                            DefaultResponses.ReplyWithGreeting(context);
                            DefaultResponses.ReplyWithHelp(context);
                            this.Greeted = true;
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
                        //case "addAlarm":
                        //    // switch to addAlarm topic
                        //    context.ConversationState.ActiveTopic = new AddAlarmTopic();
                        //    return context.ConversationState.ActiveTopic.StartTopic(context);

                        case "demoCards":
                            // switch to show alarms topic
                            context.ConversationState.ActiveTopic = new DemoCardsTopic();
                            return context.ConversationState.ActiveTopic.StartTopic(context);

                        //case "deleteAlarm":
                        //    // switch to delete alarm topic
                        //    context.ConversationState.ActiveTopic = new DeleteAlarmTopic();
                        //    return context.ConversationState.ActiveTopic.StartTopic(context);

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

        // From Bradley Lawrence
        private bool IsNewMember(IConversationUpdateActivity activity)
        {
            if (activity.MembersAdded != null && activity.MembersAdded.Any())
            {
                foreach (var member in activity.MembersAdded ?? Array.Empty<ChannelAccount>())
                {
                    if (member.Id == activity.Recipient.Id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
