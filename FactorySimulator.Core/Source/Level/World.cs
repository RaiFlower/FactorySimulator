using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.EntitySystem;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source.Level;

public class World
{
    public readonly Dictionary<Vector2, Chunk> Chunks = new();

    public readonly int ChunkSize = 16;

    public Vector2 ToTilePosition(Vector2 position)
    {
        return new Vector2(
            MathF.Floor(position.X),
            MathF.Floor(position.Y)
            );
    }

    public Vector2 ToChunkPosition(Vector2 position)
    {
        return new Vector2(
            MathF.Floor(position.X / ChunkSize),
            MathF.Floor(position.Y / ChunkSize) 
            );
    }

    public Chunk GetChunk(Vector2 position)
    {
        Vector2 chunkPosition = ToChunkPosition(position);

        if (!Chunks.ContainsKey(chunkPosition))
        {
            Chunks.Add(chunkPosition, new Chunk());
        }

        return Chunks[chunkPosition];
    }

    public void AddEntity(Entity entity)
    {
        if (SquareCast(entity.SelectionBounds, CastMode.Collision).Count == 0)
        {
            GetChunk(entity.Position).Entities.Add(entity);
        }
    }

    public void RemoveEntity(Entity entity)
    {
        GetChunk(entity.Position).Entities.Remove(entity);
    }

    public enum CastMode
    {
        Collision,
        Selection
    }

    public List<Entity> PointCast(Vector2 position, CastMode castMode)
    {
        Vector2 min = ToChunkPosition(position) - Vector2.One;
        Vector2 max = ToChunkPosition(position);

        List<Entity> result = new();

        for (float x = min.X; x <= max.X; x++)
        {
            for (float y = min.Y; y <= max.Y; y++)
            {
                GetChunk(new Vector2(x, y)).Entities.ForEach(e =>
                {
                    SquareBounds entityBoundingBox = castMode switch
                    {
                        CastMode.Collision => e.CollisionBounds,
                        CastMode.Selection => e.SelectionBounds,
                        _ => throw new ArgumentOutOfRangeException(nameof(castMode))
                    };

                    if (entityBoundingBox.Contains(position))
                    {
                        result.Add(e);
                    }
                });
            }
        }

        return result;
    }

    public List<Entity> SquareCast(SquareBounds boundingBox, CastMode castMode)
    {
        Vector2 min = ToChunkPosition(boundingBox.Min) - Vector2.One;
        Vector2 max = ToChunkPosition(boundingBox.Max);

        List<Entity> result = new();

        for (float x = min.X; x <= max.X; x++)
        {
            for (float y = min.Y; y <= max.Y; y++)
            {
                GetChunk(new Vector2(x, y)).Entities.ForEach(e =>
                {
                    SquareBounds entityBoundingBox = castMode switch
                    {
                        CastMode.Collision => e.CollisionBounds,
                        CastMode.Selection => e.SelectionBounds,
                        _ => throw new ArgumentOutOfRangeException(nameof(castMode))
                    };

                    if (entityBoundingBox.Intersects(boundingBox))
                    {
                        result.Add(e);
                    }
                });
            }
        }

        return result;
    }
}