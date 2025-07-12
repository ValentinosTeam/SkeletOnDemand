# What is this?
This simple plugin allows players to opt in to have a chance to become skeleton if they spawn in as an SCP at round start. Here is how to use it:
1. Download the latest SkeletOnDemand.dll in releases on the right and put it into the `SCP Secret Laboratory\LabAPI\plugins\7777` folder (The plugin will turn on after a restart of the server)
2. Give the players you want to have permission to opt in and out of the plugin the premission `skeletondemand`
3. Players with that permission will be able to use console commands `.skeleton` and `.skeletoff` (by default no one is opted in) to opt in and out of the plugin (their consent will be saved in `user-consent.yml`)
4. Then when the round starts, players who have opted in and happened to spawn on the SCP team will have a chance to become the skeleton (this chance can be configured under `spawn_chance` in `config.yml`)

That's the gist of it. There are also a few configurations that do a little extra.
# Configs
```yaml
# The chance for a player to become skeleton from 0 to 1 (default is 14%)
spawn_chance: 0.14000000000000001
# How many skeleton spawns this plugin will spawn. In this case, not more than 1
max_skeleton_spawns: 1
# When the player spawns in as a skeleton because of this plugin they will recieve the message bellow with this duration in seconds
hint_duration: 6
hint_message: SkeletOnDemand plugin made you skeleton this round!
```
