using System.IO;

namespace WallyMapEditor;

public class TextureCache : UploadCache<string, RlImage, Texture2DWrapper>
{
    protected override RlImage LoadIntermediate(string path)
    {
        path = Path.GetFullPath(path);
        RlImage image = WmeUtils.LoadRlImage(path);
        Rl.ImageAlphaPremultiply(ref image);
        return image;
    }

    protected override Texture2DWrapper IntermediateToValue(RlImage img)
    {
        return new(Rl.LoadTextureFromImage(img));
    }

    protected override void UnloadIntermediate(RlImage img)
    {
        Rl.UnloadImage(img);
    }

    protected override void UnloadValue(Texture2DWrapper texture)
    {
        texture.Dispose();
    }
}