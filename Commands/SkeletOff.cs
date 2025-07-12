using System;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using SkeletOnDemand.Configs;

namespace SkeletOnDemand.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class SkeletOff : ICommand
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
            response = "Opted you out from the SkeletOnDemand plugin. You will no longer have a chance to become a skeleton :(";
            SkeletOnDemand.Singleton.OptInConfig.OptIn(player.UserId, false);
            return true;

        }

        public string[] Aliases { get; } = Array.Empty<string>();
        public string Command { get; } = "skeletOff";
        public string Description { get; } =
            "Opts out from the SkeletOnDemand plugin.";
    }
}