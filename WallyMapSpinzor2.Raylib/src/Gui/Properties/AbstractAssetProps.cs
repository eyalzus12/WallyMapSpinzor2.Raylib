using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using ImGuiNET;
using rlImGui_cs;
using NativeFileDialogSharp;

namespace WallyMapSpinzor2.Raylib;

partial class PropertiesWindow
{
    public static bool ShowAbstractAssetProps(AbstractAsset a, CommandHistory cmd, RaylibCanvas? canvas, string? assetDir)
    {
        bool propChanged = false;
        if (a.AssetName is not null)
        {
            ImGui.Text("AssetName: " + a.AssetName);

            if (assetDir is not null)
            {
                ImGui.SameLine();
                if (ImGui.Button("Select"))
                {
                    Task.Run(() =>
                    {
                        DialogResult dialogResult = Dialog.FileOpen("png,jpg", assetDir);
                        if (dialogResult.IsOk)
                        {
                            string path = dialogResult.Path;
                            string newAssetName = Path.GetRelativePath(assetDir, path);
                            if (newAssetName != a.AssetName)
                            {
                                cmd.Add(new PropChangeCommand<string>(val => a.AssetName = val, a.AssetName, newAssetName));
                                propChanged = true;
                            }
                        }
                    });
                }

                if (canvas is not null)
                {
                    Texture2DWrapper texture = canvas.LoadTextureFromPath(Path.Combine(assetDir, a.AssetName));
                    rlImGui.ImageSize(texture.Texture, new Vector2(60 * (float)(texture.Width / texture.Height), 60));
                }
            }
            propChanged |= ImGuiExt.DragFloatHistory("X", a.X, val => a.X = val, cmd);
            propChanged |= ImGuiExt.DragFloatHistory("Y", a.Y, val => a.Y = val, cmd);
            ImGui.Separator();
            // proper editing of W and H alongside ScaleX and ScaleY is messy. so just do this.
            propChanged |= ImGuiExt.DragFloatHistory("W", a.W ?? 0, val => a.W = val, cmd);
            propChanged |= ImGuiExt.DragFloatHistory("H", a.H ?? 0, val => a.H = val, cmd);
            ImGui.Separator();
            propChanged |= ImGuiExt.DragFloatHistory("Rotation", a.Rotation, val => a.Rotation = BrawlhallaMath.SafeMod(val, 360.0), cmd, speed: 0.1);
        }
        else
        {
            propChanged |= ImGuiExt.DragFloatHistory("X", a.X, val => a.X = val, cmd);
            propChanged |= ImGuiExt.DragFloatHistory("Y", a.Y, val => a.Y = val, cmd);
            ImGui.Separator();
            propChanged |= ImGuiExt.DragFloatHistory("ScaleX", a.ScaleX, val => a.ScaleX = val, cmd, speed: 0.01);
            propChanged |= ImGuiExt.DragFloatHistory("ScaleY", a.ScaleY, val => a.ScaleY = val, cmd, speed: 0.01);
            ImGui.Separator();
            propChanged |= ImGuiExt.DragFloatHistory("Rotation", a.Rotation, val => a.Rotation = BrawlhallaMath.SafeMod(val, 360.0), cmd, speed: 0.1);
        }
        return propChanged;
    }
}