using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot.v4.Demo.Multi.Models;
using Bot.v4.Demo.Multi.Responses;
using Microsoft.Bot.Schema;

namespace Bot.v4.Demo.Multi.Topics
{
    public class DemoLUISTopic : ITopic
    {
        public DemoLUISTopic() { }

        public string Name { get; set; } = "LUIS Topic";

        public bool Greeted { get; set; } = false;

        public Task<bool> StartTopic(MultiBotContext context)
        {
            switch (context.Activity.Type)
            {
                case ActivityTypes.Message:
                    // greet on first message if we haven't already 
                    if (!Greeted)
                    {
                        DemoLUISResonses.ReplyWithGreeting(context);                        
                        this.Greeted = true;
                    }
                    return this.ContinueTopic(context);
            }
            return Task.FromResult(true);
        }

        public Task<bool> ContinueTopic(MultiBotContext context)
        {
            if (context.Activity.Type == ActivityTypes.Message)
            {
                switch (context.RecognizedIntents.TopIntent?.Name)
                {
                    case "help":
                        DemoLUISResonses.ReplyWithHelp(context);
                        return Task.FromResult(true);
                    case "mainMenu":
                        // prompt to go to main menu
                        // switch to the default topic
                        context.ConversationState.ActiveTopic = new DefaultTopic();
                        return context.ConversationState.ActiveTopic.StartTopic(context);
                    default:
                        // send to luis
                        DemoLUISResonses.ReplyWithLUISResult(context);
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
