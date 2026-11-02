using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.EntitySystem;
using FactorySimulator.Core.Source.SceneSystem;
using Microsoft.Xna.Framework;
using static FactorySimulator.Core.Source.Level.World;

namespace FactorySimulator.Core.Source.Level;

public class LevelScene : Scene
{
    private World _world = new();

    private Camera _camera = null!;

    private float _zoomChangeSensitivity = 1.0f / 75.0f;
    private float _normalSpeed = 8.0f;
    private float _fastSpeed = 16.0f;

    public LevelScene(string name) : base(name)
    {

    }

    public override void Start()
    {
        base.Start();

        _camera = new(_gameContextView);
    }

    public override void Update()
    {
        base.Update();

        if (_gameContextView.RegistryManager.InputActions["base:left_click"].IsPressed)
        {
            Vector2 mousePosition = _gameContextView.InputManager.MousePosition;
            Vector2 worldPosition = _world.ToTilePosition(_camera.Unproject(mousePosition));
            if (_world.PointCast(worldPosition, CastMode.Collision).Count == 0)
            {
                Entity entity = _gameContextView.RegistryManager.EntityPrototypes["base:test_entity"].BuildEntity(worldPosition, _world);
                _world.AddEntity(entity);
            }
        }

        if (_gameContextView.RegistryManager.InputActions["base:right_click"].IsPressed)
        {
            Vector2 mousePosition = _gameContextView.InputManager.MousePosition;
            Vector2 worldPosition = _camera.Unproject(mousePosition);
            _world.PointCast(worldPosition, CastMode.Selection).ForEach(_world.RemoveEntity);
        }

        _camera.Position += _gameContextView.InputManager.DirectionInput * (_gameContextView.RegistryManager.InputActions["base:shift"].IsPressed ? _fastSpeed : _normalSpeed) * _gameContextView.DeltaTime;

        _camera.Zoom += _gameContextView.InputManager.ScrollDelta * _zoomChangeSensitivity * MathF.Sqrt(_camera.Zoom);
        _camera.Zoom = MathF.Max(10.0f, MathF.Min(_camera.Zoom, 200.0f));
    }

    public override void Draw()
    {
        base.Draw();

        _world.SquareCast(_camera.ViewBounds, CastMode.Selection).ForEach(e =>
        {
            Vector2 screenPosition = _camera.Project(e.Position);
            _gameContextView.RenderManager.DrawSprite(screenPosition, e.GetSprite(out int variant), scale: _camera.IntegerZoom, variant: variant);
        });
    }
}