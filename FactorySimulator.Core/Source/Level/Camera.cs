using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.EntitySystem;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source.Level;

public class Camera
{
    private readonly GameContextView _gameContextView;

    public Vector2 Position
    {
        get => _position;

        set
        {
            _position = value;
            UpdateMatrices();
        }
    }
    private Vector2 _position = Vector2.Zero;

    public float Zoom
    {
        get => _zoom;

        set
        {
            _zoom = value;
            IntegerZoom = (int)_zoom;
            UpdateMatrices();
        }
    }
    private float _zoom = 100.0f;

    private Matrix _projectMatrix = Matrix.Identity;
    private Matrix _unprojectMatrix = Matrix.Identity;

    public Camera(GameContextView gameContextView)
    {
        _gameContextView = gameContextView;
    }

    public int IntegerZoom { get; private set; }

    private void UpdateMatrices()
    {
        _projectMatrix =
            Matrix.CreateTranslation(-Position.X, -Position.Y, 0.0f) *
            Matrix.CreateScale(IntegerZoom, -IntegerZoom, 1) *
            Matrix.CreateTranslation((int)_gameContextView.ViewportSize.X / 2, (int)_gameContextView.ViewportSize.Y / 2, 0);

        _unprojectMatrix = Matrix.Invert(_projectMatrix);
    }

    public Vector2 Project(Vector2 point)
    {
        return Vector2.Transform(point, _projectMatrix);
    }

    public Vector2 Unproject(Vector2 point)
    {
        return Vector2.Transform(point, _unprojectMatrix);
    }

    public SquareBounds ViewBounds => new
    (
        Position - new Vector2(_gameContextView.ViewportSize.X / IntegerZoom / 2, _gameContextView.ViewportSize.Y / IntegerZoom / 2), 
        Position + new Vector2(_gameContextView.ViewportSize.X / IntegerZoom / 2, _gameContextView.ViewportSize.Y / IntegerZoom / 2)
    );
}