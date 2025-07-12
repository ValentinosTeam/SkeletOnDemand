namespace SkeletOnDemand.Configs
{
    public class Config
    {
        public double SpawnChance { get; set; } = 0.14;
        public int MaxSkeletonSpawns { get; set; } = 1;
        public float HintDuration { get; set; } = 6;
        public string HintMessage { get; set; } = "SkeletOnDemand plugin made you skeleton this round!";
    }
}