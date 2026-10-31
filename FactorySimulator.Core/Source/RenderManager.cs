using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.AssetSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FactorySimulator.Core.Source;

public class RenderManager : Manager
{
    private readonly GraphicsDevice _graphicsDevice;

    private readonly SpriteBatch _spriteBatch;

    private readonly Texture2D _pixel;

    public RenderManager(GameContextView gameContextView, GraphicsDevice graphicsDevice) : base(gameContextView)
    {
        _graphicsDevice = graphicsDevice;

        _spriteBatch = new(_graphicsDevice);

        _pixel = new(_graphicsDevice, 1, 1);
        _pixel.SetData([Color.White]);
    }

    public void DrawRectangle(Vector2 position, int width, int height, int scale = 0)
    {
        _spriteBatch.Draw(
            _pixel,
            new Rectangle(
                (int)MathF.Floor(position.X),
                (int)MathF.Floor(position.Y),
                scale != 0.0f ? (width * scale) : width,
                scale != 0.0f ? (height * scale) : height
                ),
            Color.White
            );
    }

    public void DrawSprite(Vector2 position, Sprite sprite, int frame = 0, int variant = 0, float animationSpeed = 1.0f, int scale = 0)
    {
        _spriteBatch.Draw(
            sprite.Texture,
            new Rectangle(
                (int)MathF.Floor(position.X),
                (int)MathF.Floor(position.Y),
                scale != 0.0f ? sprite.FrameWidth / sprite.Scale * scale : sprite.FrameWidth,
                scale != 0.0f ? sprite.FrameHeight / sprite.Scale * scale : sprite.FrameHeight
                ),
            sprite.GetSourceRect(animationSpeed != 0.0f ? sprite.GetFrameNumber(_gameContextView.Time * animationSpeed) : frame, variant),
            Color.White
            );
    }

    public void StartFrame()
    {
        _graphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
    }

    public void EndFrame()
    {
        _spriteBatch.End();
    }
}