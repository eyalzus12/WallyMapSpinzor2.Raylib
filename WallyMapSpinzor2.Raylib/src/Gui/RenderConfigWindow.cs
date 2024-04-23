using ImGuiNET;

namespace WallyMapSpinzor2.Raylib;

public class RenderConfigWindow
{
    private bool _open = false;
    public bool Open { get => _open; set => _open = value; }

    public void Show(RenderConfig config, ref double renderSpeed)
    {
        ImGui.Begin("Render Config", ref _open);
        ImGui.SeparatorText("General##config");
        renderSpeed = ImGuiExt.DragFloat("Render speed##config", renderSpeed, speed: 0.1);

        ImGui.SeparatorText("Bounds##config");
        config.ShowCameraBounds = ImGuiExt.Checkbox("Camera bounds##config", config.ShowCameraBounds);
        config.ShowKillBounds = ImGuiExt.Checkbox("Kill bounds##config", config.ShowKillBounds);
        config.ShowSpawnBotBounds = ImGuiExt.Checkbox("Spawn bot bounds##config", config.ShowSpawnBotBounds);

        ImGui.SeparatorText("Collisions##config");
        config.ShowCollision = ImGuiExt.Checkbox("Collisions##config", config.ShowCollision);
        config.ShowCollisionNormalOverride = ImGuiExt.Checkbox("Normal overrides##config", config.ShowCollisionNormalOverride);

        ImGui.SeparatorText("Spawns##config");
        config.ShowRespawn = ImGuiExt.Checkbox("Respawns##config", config.ShowRespawn);
        config.ShowItemSpawn = ImGuiExt.Checkbox("Item spawns##config", config.ShowItemSpawn);

        ImGui.SeparatorText("Assets##config");
        config.ShowAssets = ImGuiExt.Checkbox("Assets##config", config.ShowAssets);
        config.ShowBackground = ImGuiExt.Checkbox("Backgrounds##config", config.ShowBackground);
        config.AnimatedBackgrounds = ImGuiExt.Checkbox("Animate backgrounds##config", config.AnimatedBackgrounds);

        ImGui.SeparatorText("NavNodes##config");
        config.ShowNavNode = ImGuiExt.Checkbox("NavNodes##config", config.ShowNavNode);

        ImGui.SeparatorText("Volumes##config");
        config.ShowGoal = ImGuiExt.Checkbox("Goals##config", config.ShowGoal);
        config.ShowNoDodgeZone = ImGuiExt.Checkbox("No dodge zones##config", config.ShowNoDodgeZone);
        config.ShowVolume = ImGuiExt.Checkbox("Plain volumes##config", config.ShowVolume);

        ImGui.SeparatorText("Others##config");
        config.NoSkulls = ImGuiExt.Checkbox("NoSkulls##config", config.NoSkulls);
        config.ScoringType = ImGuiExt.EnumCombo("Scoring Type##config", config.ScoringType);
        config.Theme = ImGuiExt.EnumCombo("Theme##config", config.Theme);
        config.Hotkey = ImGuiExt.EnumCombo("Hotkey##config", config.Hotkey);

        ImGui.Separator();
        if (ImGui.TreeNode("Gamemode Config##config"))
        {
            config.BlueScore = ImGuiExt.SliderInt("Blue team score##config", config.BlueScore, minValue: 0, maxValue: 99);
            config.RedScore = ImGuiExt.SliderInt("Red team score##config", config.RedScore, minValue: 0, maxValue: 99);
            ImGui.Separator();
            config.ShowPickedPlatform = ImGuiExt.Checkbox("Highlight platform king platform##config", config.ShowPickedPlatform);
            ImGuiExt.WithDisabled(!config.ShowPickedPlatform, () =>
            {
                config.PickedPlatform = ImGuiExt.SliderInt("Platform king platform index##config", config.PickedPlatform, minValue: 0, maxValue: 9);
            });
            ImGui.Separator();
            config.ShowZombieSpawns = ImGuiExt.Checkbox("Show zombie spawns##config", config.ShowZombieSpawns);
            ImGui.Separator();
            config.ShowRingRopes = ImGuiExt.Checkbox("Show brawldown ropes##config", config.ShowRingRopes);
            ImGui.Separator();
            config.ShowBombsketballTargets = ImGuiExt.Checkbox("Show bombsketball targets##config", config.ShowBombsketballTargets);
            ImGui.Separator();
            if (ImGui.TreeNode("Horde##config"))
            {
                config.ShowHordeDoors = ImGuiExt.Checkbox("Show horde doors##config", config.ShowHordeDoors);
                ImGuiExt.WithDisabled(!config.ShowHordeDoors, () =>
                {
                    // we're gonna assume for now we won't be dealing with more than 2
                    config.DamageHordeDoors[0] = ImGuiExt.SliderInt("Door 1 damage##config", config.DamageHordeDoors[0], minValue: 0, maxValue: 24);
                    config.DamageHordeDoors[1] = ImGuiExt.SliderInt("Door 2 damage##config", config.DamageHordeDoors[1], minValue: 0, maxValue: 24);
                });
                config.HordePathType = ImGuiExt.EnumCombo("Horde path type##config", config.HordePathType);
                ImGuiExt.WithDisabled(config.HordePathType == RenderConfig.PathConfigEnum.NONE, () =>
                {
                    config.HordePathIndex = ImGuiExt.SliderInt("Horde path index##config", config.HordePathIndex, 0, 19);
                    ImGuiExt.WithDisabled(config.HordePathType != RenderConfig.PathConfigEnum.CUSTOM, () =>
                    {
                        config.HordeWave = ImGuiExt.SliderInt("Horde wave##config", config.HordeWave, 0, 99);
                    });
                    ImGuiExt.WithDisabled(config.HordePathType == RenderConfig.PathConfigEnum.CUSTOM, () =>
                    {
                        config.HordeRandomSeed = (uint)ImGuiExt.DragInt("Horde random seed##config", (int)config.HordeRandomSeed, minValue: 0);
                    });
                });

                ImGui.TreePop();
            }

            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Size##config"))
        {
            config.RadiusRespawn = ImGuiExt.DragFloat("Respawn radius##config", config.RadiusRespawn, minValue: 0);
            config.RadiusZombieSpawn = ImGuiExt.DragFloat("Zombie spawn radius##config", config.RadiusZombieSpawn, minValue: 0);
            config.RadiusNavNode = ImGuiExt.DragFloat("NavNode radius##config", config.RadiusNavNode, minValue: 0);
            config.RadiusHordePathPoint = ImGuiExt.DragFloat("Horde path point radius##config", config.RadiusHordePathPoint, minValue: 0);
            config.LengthCollisionNormal = ImGuiExt.DragFloat("Collision normal length##config", config.LengthCollisionNormal, minValue: 0);
            config.OffsetNavLineArrowSide = ImGuiExt.DragFloat("Offset navnode arrow side##config", config.OffsetNavLineArrowSide, minValue: 0);
            config.OffsetNavLineArrowBack = ImGuiExt.DragFloat("Offset navnode arrow back##config", config.OffsetNavLineArrowBack);
            config.OffsetHordePathArrowSide = ImGuiExt.DragFloat("Offset horde path arrow side##config", config.OffsetHordePathArrowSide, minValue: 0);
            config.OffsetHordePathArrowBack = ImGuiExt.DragFloat("Offset horde path arrow back##config", config.OffsetHordePathArrowBack);
            ImGui.TreePop();
        }

        if (ImGui.TreeNode("Colors##Colors"))
        {
            if (ImGui.TreeNode("Bounds##configColors"))
            {
                config.ColorCameraBounds = ImGuiExt.ColorPicker4("Camera bounds##configColors", config.ColorCameraBounds);
                config.ColorKillBounds = ImGuiExt.ColorPicker4("Kill bounds##configColors", config.ColorKillBounds);
                config.ColorSpawnBotBounds = ImGuiExt.ColorPicker4("Spawn bot bounds##configColors", config.ColorSpawnBotBounds);
                ImGui.TreePop();
            }
            if (ImGui.TreeNode("Collisions##configColors"))
            {
                config.ColorHardCollision = ImGuiExt.ColorPicker4("Hard##configColors", config.ColorHardCollision);
                config.ColorSoftCollision = ImGuiExt.ColorPicker4("Soft##configColors", config.ColorSoftCollision);
                config.ColorGameModeHardCollision = ImGuiExt.ColorPicker4("Gamemode hard##configColors", config.ColorGameModeHardCollision);
                config.ColorBouncyHardCollision = ImGuiExt.ColorPicker4("Bouncy hard##configColors", config.ColorBouncyHardCollision);
                config.ColorBouncySoftCollision = ImGuiExt.ColorPicker4("Bouncy soft##configColors", config.ColorBouncySoftCollision);
                config.ColorBouncyNoSlideCollision = ImGuiExt.ColorPicker4("No slide##configColors", config.ColorNoSlideCollision);
                config.ColorTriggerCollision = ImGuiExt.ColorPicker4("Trigger##configColors", config.ColorTriggerCollision);
                config.ColorStickyCollision = ImGuiExt.ColorPicker4("Sticky##configColors", config.ColorStickyCollision);
                config.ColorItemIgnoreCollision = ImGuiExt.ColorPicker4("Item ignore##configColors", config.ColorItemIgnoreCollision);
                config.ColorPressurePlateCollision = ImGuiExt.ColorPicker4("Pressure plate##configColors", config.ColorPressurePlateCollision);
                config.ColorSoftPressurePlateCollision = ImGuiExt.ColorPicker4("Soft pressure plate##configColors", config.ColorSoftPressurePlateCollision);
                config.ColorLavaCollision = ImGuiExt.ColorPicker4("Lava##configColors", config.ColorLavaCollision);
                config.ColorCollisionNormal = ImGuiExt.ColorPicker4("Collision normal##configColors", config.ColorCollisionNormal);
                ImGui.TreePop();
            }
            if (ImGui.TreeNode("Respawns##configColors"))
            {
                config.ColorRespawn = ImGuiExt.ColorPicker4("Respawn##configColors", config.ColorRespawn);
                config.ColorInitialRespawn = ImGuiExt.ColorPicker4("Initial respawn##configColors", config.ColorInitialRespawn);
                config.ColorExpandedInitRespawn = ImGuiExt.ColorPicker4("Expanded init respawn##configColors", config.ColorExpandedInitRespawn);
                config.ColorZombieSpawns = ImGuiExt.ColorPicker4("Zombie spawns##configColors", config.ColorZombieSpawns);
                ImGui.TreePop();
            }
            if (ImGui.TreeNode("Item spawns##configColors"))
            {
                config.ColorItemSpawn = ImGuiExt.ColorPicker4("Item spawn##configColors", config.ColorItemSpawn);
                config.ColorItemInitSpawn = ImGuiExt.ColorPicker4("Item init spawn##configColors", config.ColorItemInitSpawn);
                config.ColorItemSet = ImGuiExt.ColorPicker4("Item set##configColors", config.ColorItemSet);
                config.ColorTeamItemInitSpawn = ImGuiExt.ColorPicker4("Team item init spawn##configColors", config.ColorTeamItemInitSpawn);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("NavNodes##configColors"))
            {
                config.ColorNavNode = ImGuiExt.ColorPicker4("NavNode##configColors", config.ColorNavNode);
                config.ColorNavNodeW = ImGuiExt.ColorPicker4("NavNodeW##configColors", config.ColorNavNodeW);
                config.ColorNavNodeL = ImGuiExt.ColorPicker4("NavNodeL##configColors", config.ColorNavNodeL);
                config.ColorNavNodeA = ImGuiExt.ColorPicker4("NavNodeA##configColors", config.ColorNavNodeA);
                config.ColorNavNodeG = ImGuiExt.ColorPicker4("NavNodeG##configColors", config.ColorNavNodeG);
                config.ColorNavNodeT = ImGuiExt.ColorPicker4("NavNodeT##configColors", config.ColorNavNodeT);
                config.ColorNavNodeS = ImGuiExt.ColorPicker4("NavNodeS##configColors", config.ColorNavNodeS);
                config.ColorNavPath = ImGuiExt.ColorPicker4("NavPath##configColors", config.ColorNavPath);
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Team##configColors"))
            {
                for (int i = 0; i < config.ColorVolumeTeam.Length; i++)
                {
                    config.ColorVolumeTeam[i] = ImGuiExt.ColorPicker4($"Team {i} volume##configColors", config.ColorVolumeTeam[i]);
                }
                ImGui.Separator();
                for (int i = 0; i < config.ColorCollisionTeam.Length; i++)
                {
                    config.ColorCollisionTeam[i] = ImGuiExt.ColorPicker4($"Team {i + 1} collision##configColors", config.ColorCollisionTeam[i]);
                }
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Gamemodes##configColors"))
            {
                config.ColorHordePath = ImGuiExt.ColorPicker4("Horde path##configColors", config.ColorHordePath);
                ImGui.TreePop();
            }

            ImGui.TreePop();
        }

        ImGui.End();
    }
}