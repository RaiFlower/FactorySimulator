using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace FactorySimulator.Core.Source.AssetSystem;

public class AssetManager : Manager
{
    private string _assetFolderPath;

    private GraphicsDevice _graphicsDevice;

    public Dictionary<string, Sprite> Sprites = new();

    private AtlasParameters _defaultAtlasParameters = new(100, 1, 1, 12);

    public AssetManager(GameContextView gameContextView, GraphicsDevice graphicsDevice) : base(gameContextView)
    {
        _graphicsDevice = graphicsDevice;

        _assetFolderPath = Path.Combine(AppContext.BaseDirectory, "assets");
    }

    public void CreateAssets()
    {
        foreach (string absolutePath in Directory.EnumerateFiles(_assetFolderPath, "*", SearchOption.AllDirectories).ToList())
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
                    Sprite sprite = BuildSprite(id, absolutePath);
                    sprite.Load(_graphicsDevice);
                    Sprites.Add(id, sprite);
                    break;
            }
        }

        _gameContextView.RegistryManager.LoadAssetReferences();
    }

    public class AtlasParameters
    {
        public int Scale { get; init; }
        public int Frames { get; init; }
        public int Variants { get; init; }
        public int FPS { get; init; }

        public AtlasParameters(int scale, int frames, int variants, int fps)
        {
            Scale = scale;
            Frames = frames;
            Variants = variants;
            FPS = fps;
        }
    }

    private Sprite BuildSprite(string id, string path)
    {
        string jsonPath = Path.ChangeExtension(path, "json");
        AtlasParameters parameters;

        if (!Path.Exists(jsonPath))
        {
            parameters = _defaultAtlasParameters;
        }
        else
        {
            parameters = JsonSerializer.Deserialize<AtlasParameters>(File.ReadAllText(jsonPath))!;
        }

        return new Sprite(
            id, 
            path, 
            parameters.Scale,
            parameters.Frames, 
            parameters.Variants, 
            parameters.FPS
            );
    }
}