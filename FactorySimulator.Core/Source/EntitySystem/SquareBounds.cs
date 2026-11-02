using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source.EntitySystem;

public class SquareBounds
{
    public readonly Vector2 Min;
    public readonly Vector2 Max;

    public SquareBounds(Vector2 min, Vector2 max)
    {
        Min = min;
        Max = max;
    }

    public bool Contains(Vector2 point)
    {
        return
            point.X > Min.X &&
            point.Y > Min.Y &&
            point.X < Max.X &&
            point.Y < Max.Y;
    }

    public bool Intersects(SquareBounds squareBounds)
    {
        return
            squareBounds.Max.X > Min.X && 
            squareBounds.Max.Y > Min.Y && 
            squareBounds.Min.X < Max.X && 
            squareBounds.Min.Y < Max.Y;
    }
}