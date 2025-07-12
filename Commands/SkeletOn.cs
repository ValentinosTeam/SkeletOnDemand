using System;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using SkeletOnDemand.Configs;

namespace SkeletOnDemand.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class SkeletOn : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.HasPermissions(OptInConfig.Permission))
            {
                response = "You don't have permission to use this command! (Players Management)";
                return false;
            }
            if (!Player.TryGet(sender, out Player player))
            {
                response = "You must be a player to use this command!";
                return false;
            }
            if (player.DoNotTrack)
            {
                response = "Can't track your consent, since you have DNT enabled :(";
                return false;
            }
            response = "You will have a chance to become a skeleton when you start the round as an SCP.";
            SkeletOnDemand.Singleton.OptInConfig.OptIn(player.UserId, true);
            return true;

        }

        public string[] Aliases { get; } = Array.Empty<string>();
        public string Command { get; } = "skeletOn";
        public string Description { get; } =
            "Opts in to the SkeletOnDemand plugin. You will have a chance to become a skeleton when you start the round as an SCP.";
    }
}