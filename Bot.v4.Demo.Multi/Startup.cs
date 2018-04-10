using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Builder.LUIS;
using System;
using Microsoft.Cognitive.LUIS;
using System.Text.RegularExpressions;
using Bot.v4.Demo.Multi.Models;

namespace Bot.v4.Demo.Multi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => Configuration);
            services.AddBot<MultiBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);
                var middleware = options.Middleware;
                middleware.Add(new ConversationState<ConversationData>(new MemoryStorage()));

                // Regex Intents
                middleware.Add(new RegExpRecognizerMiddleware()
                                .AddIntent("mainMenu", new Regex("main menu(.*)", RegexOptions.IgnoreCase))
                                .AddIntent("help", new Regex("help(.*)", RegexOptions.IgnoreCase))
                                .AddIntent("cancel", new Regex("cancel(.*)", RegexOptions.IgnoreCase)));

                // Setup LUIS Middleware
                var luisRecognizerOptions = new LuisRecognizerOptions { Verbose = false };
                var luisModel = new LuisModel(
                    Configuration["LUIS:AppId"],
                    Configuration["LUIS:SubscriptionKey"],
                    new Uri("https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/"));
                middleware.Add(new LuisRecognizerMiddleware(luisModel, luisRecognizerOptions));

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseBotFramework();
        }
    }
}
