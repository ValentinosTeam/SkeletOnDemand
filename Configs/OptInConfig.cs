using System.Collections.Generic;
using LabApi.Loader;

namespace SkeletOnDemand.Configs
{
    public class OptInConfig
    {
        public const string ConfigName = "user-consent.yml";
        public const string Permission = "skeletondemand";
        // public static readonly float SpawnChance = SkeletOnDemand.Singleton.Config
        public Dictionary<string, bool> PlayerConsent { get; set; } = new Dictionary<string, bool>();

        public bool PlayerOptedIn(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;
            return (PlayerConsent.TryGetValue(userId, out bool optedIn) && optedIn);
        }

        public void OptIn(string userId, bool value)
        {
            PlayerConsent[userId] = value;
            SkeletOnDemand.Singleton.SaveConfig(this, ConfigName);
        }
    }
}