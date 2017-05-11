using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DragonBot
{
    class MyBot
    {
        DiscordClient discord;
        Random rand;
        public MyBot()
        {
            rand = new Random();
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });
            // COMMANDS
            var commands = discord.GetService<CommandService>();  

            commands.CreateCommand("Hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("╭∩╮( ͡° ͜ʖ ͡°)");
                });

            commands.CreateCommand("roll")
                .Do(async (e) =>
                {
                    int a = 0;
                    await e.Channel.SendMessage("Roll the bones");
                    while (a < 5)
                    {
                        int b;
                        b = rand.Next(1,5);
                        await e.Channel.SendMessage(b.ToString() );
                        System.Threading.Thread.Sleep(150);
                        a++;
                    }
                });
            // EXECUTE
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzEyMTQ4NzUwMTQ1NDIxMzEz.C_W5sw.DTrZKzykj_VPk0Br_28AZ-jV6GM", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
