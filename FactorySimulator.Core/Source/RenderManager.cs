using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FactorySimulator.Core.Source;

public class RenderManager : Manager
{
    private readonly GraphicsDevice _graphicsDevice;

    private readonly SpriteBatch _spriteBatch;

    public RenderManager(GameContextView gameContextView, GraphicsDevice graphicsDevice) : base(gameContextView)
    {
        _graphicsDevice = graphicsDevice;

        _spriteBatch = new(_graphicsDevice);
    }

    public void DrawSquare(Vector2 pos, int size)
    {
        _spriteBatch.Draw(_gameContextView.AssetManager.Sprites["base:missing_texture"].Texture, new Rectangle((int)pos.X, (int)pos.Y, size, size), Color.White);
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