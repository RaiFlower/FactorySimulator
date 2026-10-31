using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.SceneSystem;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source.Level;

public class LevelScene : Scene
{
    private World _world = new();

    private Camera _camera = null!;

    private float _zoomChangeSensitivity = 1.0f / 75.0f;
    private float _speed = 7.0f;

    public LevelScene(string name) : base(name)
    {
        
    }

    public override void Start()
    {
        base.Start();

        _camera = new();

        _gameContextView.RegistryManager.InputActions["base:left_click"].OnPress += () =>
        {
            Vector2 mousePosition = _gameContextView.InputManager.MousePosition;
            Vector2 worldPosition = _world.ToTilePosition(_camera.Unproject(mousePosition));
            _world.Entities.Add(worldPosition, new());
        };
    }

    public override void Update()
    {
        base.Update();

        _camera.Position += _gameContextView.InputManager.DirectionInput * _speed * _gameContextView.DeltaTime;

        _camera.Zoom += _gameContextView.InputManager.ScrollDelta * _zoomChangeSensitivity * MathF.Sqrt(_camera.Zoom);
        _camera.Zoom = MathF.Max(10.0f, MathF.Min(_camera.Zoom, 200.0f));
    }

    public override void Draw()
    {
        base.Draw();

        _world.Entities.Keys.ToList().ForEach(s =>
        {
            Vector2 screenPosition = _camera.Project(s);
            _gameContextView.RenderManager.DrawSprite(screenPosition, _gameContextView.AssetManager.Sprites["base:missing_texture"], scale: _camera.IntZoom);
        });
    }
}