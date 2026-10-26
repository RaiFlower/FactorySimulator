using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace FactorySimulator.Core.Source.AssetSystem;

public class AssetManager : Manager
{
    private string _assetFolderPath;

    private GraphicsDevice _graphicsDevice;

    public Dictionary<string, Sprite> Sprites = new();

    public AssetManager(GameContextView gameContextView, GraphicsDevice graphicsDevice) : base(gameContextView)
    {
        _graphicsDevice = graphicsDevice;

        _assetFolderPath = Path.Combine(AppContext.BaseDirectory, "Assets");
    }

    public void LoadAssets()
    {
        foreach (string absolutePath in Directory.EnumerateFiles(_assetFolderPath, "*", SearchOption.AllDirectories))
        {
            string relativePath = Path.GetRelativePath(_assetFolderPath, absolutePath).Replace('\\', '/');

            int index = relativePath.IndexOf('/');

            if (index < 0)
            {
                continue;
            }

            string moduleName = relativePath[..index];
            string assetName = Path.GetFileNameWithoutExtension(absolutePath);

            string id = $"{moduleName}:{assetName}";

            switch (Path.GetExtension(absolutePath).ToLowerInvariant())
            {
                case ".png":
                    Sprite sprite = new Sprite(id, absolutePath);
                    sprite.Load(_graphicsDevice);
                    Sprites.Add(id, sprite);
                    break;
            }
        }
    }
}