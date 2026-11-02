using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.AssetSystem;
using FactorySimulator.Core.Source.Level;
using FactorySimulator.Core.Source.RegistrySystem;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source.EntitySystem;

public class EntityPrototype : ContentItem
{
    public Collider CollisionBox { get; init; }
    public Collider SelectionBox { get; init; }

    public Sprite Sprite { get; private set; } = null!;

    public string SpriteName { get; init; }

    public Vector2 PlacementOffset { get; init; }

    public EntityPrototype(string name, Collider collisionBox, Collider selectionBox, Vector2 placementOffset, string spriteName) : base(name)
    {
        CollisionBox = collisionBox;
        SelectionBox = selectionBox;

        PlacementOffset = placementOffset;

        SpriteName = spriteName;
    }

    public override void LoadContentReferences()
    {
        base.LoadContentReferences();
    }

    public override void LoadAssetReferences()
    {
        base.LoadAssetReferences();

        Sprite = _gameContextView.AssetManager.Sprites[SpriteName];
    }

    public virtual Entity BuildEntity(Vector2 position, World world)
    {
        return new(position - PlacementOffset, world, this);
    }
}

public class Entity
{
    public EntityPrototype Protype;

    public World World;
    public Vector2 Position;

    public Entity(Vector2 position, World world, EntityPrototype protype)
    {
        Protype = protype;

        Position = position;
        World = world;
    }

    public Sprite GetSprite(out int variant)
    {
        variant = 0;
        return Protype.Sprite;
    }

    public SquareBounds CollisionBounds => Protype.CollisionBox.GetBoundingBox(Position);
    public SquareBounds SelectionBounds => Protype.SelectionBox.GetBoundingBox(Position);
}