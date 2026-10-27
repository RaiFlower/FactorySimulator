using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace FactorySimulator.Core.Source.InputSystem;

public class InputSnapshot
{
    public readonly KeyboardState CurrentKeyState;
    public readonly MouseState CurrentMouseState;

    public InputSnapshot(ref KeyboardState currentKeyState, ref MouseState currentMouseState)
    {
        CurrentKeyState = currentKeyState;
        CurrentMouseState = currentMouseState;
    }
}

public abstract class InputBinding
{
    public bool IsPressed { get; protected set; }
    public bool WasPressed { get; protected set; }

    public abstract void Update(InputSnapshot inputSnapshot);
}

public class KeyBinding : InputBinding
{
    public readonly Keys Key;

    public KeyBinding(Keys key)
    {
        Key = key;
    }

    public override void Update(InputSnapshot inputSnapshot)
    {
        WasPressed = IsPressed;
        IsPressed = inputSnapshot.CurrentKeyState.IsKeyDown(Key);
    }
}

public enum MouseButton
{
    Left,
    Middle,
    Right
}

public class MouseBinding : InputBinding
{
    public readonly MouseButton Button;

    public MouseBinding(MouseButton button)
    {
        Button = button;
    }

    public override void Update(InputSnapshot inputSnapshot)
    {
        WasPressed = IsPressed;
        IsPressed = Button switch
        {
            MouseButton.Left => inputSnapshot.CurrentMouseState.LeftButton == ButtonState.Pressed,
            MouseButton.Middle => inputSnapshot.CurrentMouseState.MiddleButton == ButtonState.Pressed,
            MouseButton.Right => inputSnapshot.CurrentMouseState.RightButton == ButtonState.Pressed,
            _ => false
        };
    }
}