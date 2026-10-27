using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FactorySimulator.Core.Source.InputSystem;

public class InputManager : Manager
{
    private KeyboardState _previousKeyState;
    private MouseState _previousMouseState;

    private KeyboardState _currentKeyState;
    private MouseState _currentMouseState;

    public Vector2 DirectionInput { get; private set; }

    private InputAction _upAction = null!;
    private InputAction _downAction = null!;
    private InputAction _leftAction = null!;
    private InputAction _rightAction = null!;

    public Vector2 MousePosition { get; private set; }
    public int ScrollDelta { get; private set; }

    public InputManager(GameContextView gameContextView) : base(gameContextView)
    {
        _currentKeyState = Keyboard.GetState();
        _currentMouseState = Mouse.GetState();
    }

    public override void Init()
    {
        base.Init();

        _upAction = _gameContextView.RegistryManager.InputActions["up"];
        _downAction = _gameContextView.RegistryManager.InputActions["down"];
        _leftAction = _gameContextView.RegistryManager.InputActions["left"];
        _rightAction = _gameContextView.RegistryManager.InputActions["right"];
    }

    public override void Update()
    {
        base.Update();

        _previousKeyState = _currentKeyState;
        _previousMouseState = _currentMouseState;

        _currentKeyState = Keyboard.GetState();
        _currentMouseState = Mouse.GetState();

        foreach (InputAction action in _gameContextView.RegistryManager.InputActions.Values)
        {
            action.Update(new(ref _currentKeyState, ref _currentMouseState));
        }

        DirectionInput = new(
            (_rightAction.IsPressed ? 1 : 0) + (_leftAction.IsPressed ? -1 : 0),
            (_upAction.IsPressed ? 1 : 0) + (_downAction.IsPressed ? -1 : 0)
        );

        if (DirectionInput.LengthSquared() > 1.0f)
        {
            DirectionInput = Vector2.Normalize(DirectionInput);
        }

        MousePosition = new(_currentMouseState.Position.X, _currentMouseState.Position.Y);
        ScrollDelta = _currentMouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue;
    }
}