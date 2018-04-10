using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.LUIS;
using Microsoft.Bot;
using Bot.v4.Demo.Multi.Models;
using Bot.v4.Demo.Multi.Topics;

namespace Bot.v4.Demo.Multi
{
    public class MultiBot : IBot
    {
        
        public async Task OnTurn(ITurnContext botContext)
        {
            var context = new MultiBotContext(botContext);

            bool handled = false;

            // no active topic
            if (context.ConversationState.ActiveTopic == null)
            {
                // use default
                context.ConversationState.ActiveTopic = new DefaultTopic();
                handled = await context.ConversationState.ActiveTopic.StartTopic(context);
            }
            else
            {
                // we have an active topic
                handled = await context.ConversationState.ActiveTopic.ContinueTopic(context);
            }

            // if activeTopic's result is false and the activeTopic is NOT already the default topic
            if (handled == false && !(context.ConversationState.ActiveTopic is DefaultTopic))
            {
                // USe DefaultTopic as the active topic
                context.ConversationState.ActiveTopic = new DefaultTopic();
                handled = await context.ConversationState.ActiveTopic.ResumeTopic(context);
            }

        }

    }

}