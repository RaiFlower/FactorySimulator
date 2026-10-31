using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source.Level;

public class Camera
{
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
            IntZoom = (int)_zoom;
            UpdateMatrices();
        }
    }
    private float _zoom = 100.0f;

    private Matrix _projectMatrix = Matrix.Identity;
    private Matrix _unprojectMatrix = Matrix.Identity;

    public int IntZoom { get; private set; }

    private void UpdateMatrices()
    {
        _projectMatrix =
            Matrix.CreateTranslation(Position.X * -1.0f, Position.Y, 0.0f) *
            Matrix.CreateScale(IntZoom) *
            Matrix.CreateTranslation(1280 / 2, 720 / 2, 0);

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
}