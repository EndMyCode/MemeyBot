using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Audio;

namespace DiscordBot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("help")]
        [Summary("Tells you the commands")]
        public async Task Help()
        {
            var info = new EmbedFieldBuilder()
                .WithName("Infos")
                .WithValue("If you want to use a multi word argument, you have to put it in quotation marks. (example: ;nick @User \"Name Goes Here\"")
                .WithIsInline(true);
            var command1 = new EmbedFieldBuilder()
                .WithName("help")
                .WithValue("Shows commands.")
                .WithIsInline(true);
            var command2 = new EmbedFieldBuilder()
                .WithName("test")
                .WithValue("Tests if the bot is working, by repeating what you said.")
                .WithIsInline(true);
            var command3 = new EmbedFieldBuilder()
                .WithName("insult")
                .WithValue("Insults someone.")
                .WithIsInline(true);
            var command4 = new EmbedFieldBuilder()
                .WithName("nick (nickname)")
                .WithValue("Changes the nickname of a person(requires the change nickname permission.")
                .WithIsInline(true);
            var embed = new EmbedBuilder()
                .WithAuthor(Context.Client.CurrentUser)
                .AddField(info)
                .AddField(command1)
                .AddField(command2)
                .AddField(command3)
                .AddField(command4)
                .Build();

            await ReplyAsync(embed: embed);


        }

        [Command("test")]
        [Summary("If the bot doesnt react, use this command to see if its enabled.")]
        public Task TestAsync([Remainder][Summary("The said text.")] string echo)
            => ReplyAsync(echo);

        [Command("insult")]
        [Summary("Insults someone.")]
        public async Task BeleidigungAsync([Summary("The insulted user.")] SocketGuildUser user = null)
        {
            await user.SendMessageAsync($"{Context.Message.Author} hat mir gesagt dass ich dir sagen soll, du sollst dich ficken.");
        }

        [Command("nick")]
        [Summary("changes nickname.")]
        [Alias("nickname")]
        [RequireUserPermission(GuildPermission.ChangeNickname)]
        public async Task NickAsync([Summary("User to rename.")] SocketGuildUser user = null, [Summary("The new name.")] string name = null)
        {
            await user.ModifyAsync(x =>
            {
                x.Nickname = name;
            });
        }
    }
}