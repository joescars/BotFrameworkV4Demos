using Bot.v4.Demo.Multi.Models;
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
            switch (context.Request.Type)
            {
                case ActivityTypes.ConversationUpdate:
                    {
                        // greet when added to conversation
                        var activity = context.Request.AsConversationUpdateActivity();
                        if (activity.MembersAdded.Where(m => m.Id == activity.Recipient.Id).Any())
                        {
                            DemoCardsResponses.ReplyWithGreeting(context);
                            this.Greeted = true;
                        }
                    }
                    break;

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
            if (context.Request.Type == ActivityTypes.Message)
            {
                switch (context.RecognizedIntents.TopIntent?.Name)
                {
                    //case "showAlarms":
                    //    // allow show alarm to interrupt, but it's one turn, so we show the data without changing the topic
                    //    await new ShowAlarmsTopic().StartTopic(context);
                    //    return await this.PromptForMissingData(context);

                    case "showCarousel":
                        DemoCardsResponses.ReplyWithCarousel(context);
                        return Task.FromResult(true);

                    case "showAttachment":
                        DemoCardsResponses.ReplyWithAttachment(context);
                        return Task.FromResult(true);

                    case "help":
                        // show contextual help 
                        DemoCardsResponses.ReplyWithHelp(context);
                        return Task.FromResult(true);

                    case "cancel":
                        // prompt to cancel
                        DemoCardsResponses.ReplyWithConfused(context);
                        return Task.FromResult(true);

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
            throw new System.NotImplementedException();
        }


        public static Task ShowAlarms(MultiBotContext context)
        {
            //ShowAlarmsResponses.ReplyWithShowAlarms(context, context.UserState.Alarms);
            return Task.CompletedTask;
        }
    }
}
