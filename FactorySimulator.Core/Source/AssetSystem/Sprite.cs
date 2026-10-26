using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.RegistrySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FactorySimulator.Core.Source.AssetSystem;

public class Sprite : AssetItem
{
    public Texture2D Texture { get; private set; } = null!;

    public Sprite(string name, string path) : base(name, path)
    {

    }

    public override void Load(GraphicsDevice graphicsDevice)
    {
        base.Load(graphicsDevice);

        using var stream = File.OpenRead(Path);
        Texture = Texture2D.FromStream(graphicsDevice, stream);
        
        var data = new Color[Texture.Width * Texture.Height];
        
        Texture.GetData(data);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Color.FromNonPremultiplied(data[i].R, data[i].G, data[i].B, data[i].A);
        }

        Texture.SetData(data);
    }
}