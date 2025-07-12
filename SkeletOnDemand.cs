using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using SkeletOnDemand.Configs;

namespace SkeletOnDemand
{
    public class SkeletOnDemand : Plugin<Config>
    {
        public static SkeletOnDemand Singleton { get; private set; }
        public EventListener EventListener { get; private set; }
        public OptInConfig OptInConfig { get; private set; }

        public override void Enable()
        {
            Singleton = this;
            EventListener = Config != null ? new EventListener(Config.MaxSkeletonSpawns, Config.SpawnChance) : new EventListener();
            CustomHandlersManager.RegisterEventsHandler(EventListener);
        }

        public override void LoadConfigs()
        {
            base.LoadConfigs();
            OptInConfig = this.LoadConfig<OptInConfig>(OptInConfig.ConfigName);
        }

        public override void Disable()
        {
            CustomHandlersManager.UnregisterEventsHandler(EventListener);
        }

        public override string Name { get; } = "SkeletOnDemand";
        public override string Description { get; } =
            "Allows users to use a command to opt in to this plugin to have a chance to become a skeleton when they spawn in as an SCP";
        public override string Author { get; } = "Alex_Joo";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);
    }
}