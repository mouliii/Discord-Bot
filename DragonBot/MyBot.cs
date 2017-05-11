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
        CommandService commands;
        Random rand;

        string[] commandNames = new string[] {"hello", "roll", "mount" };
        int[] b = new int[5];

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
            commands = discord.GetService<CommandService>();

            commands.CreateCommand("Hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("╭∩╮( ͡° ͜ʖ ͡°)");
                });

            commands.CreateCommand("roll")
                .Do(async (e) =>
                {
                    int sum = 0;
                    for (int i = 0; i < b.Length; i++)
                    {
                        b[i] = rand.Next(1, 6);
                        sum += b[i];
                    }
                    Array.Sort(b);
                    string result = "{ " + b[0] + " " + b[1] + " " + b[2] + " " + b[3] + " " + b[4] + " }";
                    await e.Channel.SendMessage(result);
                    Console.WriteLine(result);
                    System.Threading.Thread.Sleep(200);
                    await e.Channel.SendMessage("Total: "+sum.ToString() );
                    if (sum < 10 && sum > 1)
                    {
                        await e.Channel.SendMessage(":monkas:");
                    }
                    else if(sum == 6)
                    {
                        await e.Channel.SendMessage(":wutface:");
                    }
                    else if (sum > 20)
                    {
                        await e.Channel.SendMessage(":PogChamp:");
                    }
                });

            commands.CreateCommand("mount")
                .Do(async (e) =>
                {
                    int m = 0;
                    int attempts = 0;
                    while (m != 1)
                    {
                        m = rand.Next(1, 100);
                        attempts++;
                    }
                    await e.Channel.SendMessage(attempts.ToString() + " attempts" );
                });

            commands.CreateCommand("help")
                .Do(async (e) =>
                {
                    string asd = "";
                    await e.Channel.SendMessage("Commands are:");
                    for (int i = 0; i < commandNames.Length; i++)
                    {
                        asd += commandNames[i] + ", ";
                    }
                    await e.Channel.SendMessage(asd);
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
