using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Bot.v4.Demo.Multi.Models;

namespace Bot.v4.Demo.Multi
{
    public interface ITopic
    {
        string Name { get; set; }
        Task<bool> StartTopic(MultiBotContext context);
        Task<bool> ContinueTopic(MultiBotContext context);
        Task<bool> ResumeTopic(MultiBotContext context);
    }
}
