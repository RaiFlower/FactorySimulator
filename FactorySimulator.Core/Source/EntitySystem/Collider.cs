using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source.EntitySystem;

public class Collider
{
    public Vector2 Size { get; init; }
    public Vector2 Offset { get; init; }

    public Collider(Vector2 size)
    {
        Size = size;
        Offset = Vector2.Zero;
    }

    public Collider(Vector2 size, Vector2 offset)
    {
        Size = size;
        Offset = offset;
    }

    public SquareBounds GetBoundingBox(Vector2 position)
    {
        return new SquareBounds(
            position + Offset,
            position + Offset + Size
        );
    }
}