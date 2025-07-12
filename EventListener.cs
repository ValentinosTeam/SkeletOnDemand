using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using SkeletOnDemand.Configs;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
using Logger = LabApi.Features.Console.Logger;
using Random = System.Random;

namespace SkeletOnDemand
{
    public class EventListener : CustomEventsHandler
    {
        private readonly Random _random = new Random();
        private readonly double _spawnChance;
        private readonly int _maxSkeletonSpawns;
        private int _skeletonSpawns = 0;

        public EventListener(int maxSkeletonSpawns, double spawnChance)
        {
            _maxSkeletonSpawns = maxSkeletonSpawns;
            _spawnChance = spawnChance;
        }
        public EventListener()
        {
            _maxSkeletonSpawns = 1;
            _spawnChance = 0.14;
        }

        public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
        {
            Player player = ev.Player;
            if (player.DoNotTrack)
            {
                SkeletOnDemand.Singleton.OptInConfig.PlayerConsent.Remove(player.UserId);
            }
        }

        public override void OnServerRoundStarted()
        {
            _skeletonSpawns = 0;
        }

        public override void OnPlayerSpawned(PlayerSpawnedEventArgs ev)
        {
            // Logger.Info("0. Event Triggered");
            Player player = ev.Player;
            PlayerRoleBase role = ev.Role;
            if (_skeletonSpawns >= _maxSkeletonSpawns) return;
            // Logger.Info("1. " + _skeletonSpawns + " out of " + _maxSkeletonSpawns);
            if (role.ServerSpawnReason != RoleChangeReason.RoundStart) return;
            // Logger.Info("2. Spawned at Round Start");
            if (role.Team != Team.SCPs) return;
            // Logger.Info("3. Is on Team SCP");
            if (role.RoleTypeId == RoleTypeId.Scp0492 || role.RoleTypeId == RoleTypeId.Scp3114) return;
            // Logger.Info("4. Is not a zombie or a skelly");
            if (!player.HasPermissions(OptInConfig.Permission)) return;
            // Logger.Info("5. Has permission for skeletondemand");
            if (!SkeletOnDemand.Singleton.OptInConfig.PlayerOptedIn(player.UserId)) return;
            // Logger.Info("6. Has opted in");
            if (!(_random.NextDouble() < _spawnChance)) return;
            // Logger.Info("7. Passed the random check, spawning him as skelly...");
            SpawnSkelly(player);
        }
        private void SpawnSkelly(Player player)
        {
            _skeletonSpawns++;
            float duration = 6f;
            string hintMessage = "SkeletOnDemand plugin made you skeleton this round!";
            if (SkeletOnDemand.Singleton.Config != null)
            {
                duration = SkeletOnDemand.Singleton.Config.HintDuration;
                hintMessage = SkeletOnDemand.Singleton.Config.HintMessage;
            }
            Timing.CallDelayed(0.1f, () =>
            {
                player.SetRole(RoleTypeId.Scp3114, RoleChangeReason.RemoteAdmin, RoleSpawnFlags.All);
                player.SendHint(hintMessage, duration);
                Logger.Info($"Spawned player {player.Nickname} ({player.UserId}) as a Skeleton this round! {_skeletonSpawns}/{_maxSkeletonSpawns}");
                // Logger.Info("8. DONE!");
            });
        }
    }
}
