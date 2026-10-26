using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.RegistrySystem;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FactorySimulator.Core.Source.AssetSystem;

public class AssetItem
{
    public readonly string Name;
    public readonly string Path;

    public AssetItem(string name, string path)
    {
        Name = name;
        Path = path;
    }

    public virtual void Load(GraphicsDevice graphicsDevice) { }
}