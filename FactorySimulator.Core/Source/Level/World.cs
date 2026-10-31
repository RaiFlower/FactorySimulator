using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.EntitySystem;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source.Level;

public class World
{
    public readonly Dictionary<Vector2, Entity> Entities = new();

    public Vector2 ToTilePosition(Vector2 position)
    {
        return new Vector2(
            MathF.Floor(position.X), 
            MathF.Floor(position.Y)
            );
    }
}