using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.v4.Demo.Multi.Models
{
    public class MultiBotContext : TurnContextWrapper
    {
        public MultiBotContext(ITurnContext context) : base(context)
        {
        }

        /// <summary>
        /// Persisted MultiBot Conversation State 
        /// </summary>
        public ConversationData ConversationState
        {
            get
            {
                return ConversationState<ConversationData>.Get(this);
            }
        }

        /// <summary>
        /// Persisted MultiBot User State
        /// </summary>
        public UserData UserState
        {
            get
            {
                return UserState<UserData>.Get(this);
            }
        }

        public IRecognizedIntents RecognizedIntents { get { return this.Get<IRecognizedIntents>(); } }
    }
}
