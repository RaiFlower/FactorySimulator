using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.SceneSystem;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source.Level;

public class LevelScene : Scene
{
    public LevelScene(string name) : base(name)
    {
        
    }

    public override void Draw()
    {
        base.Draw();

        GameContextView.RenderManager.DrawSquare(new Vector2(100.0f, 100.0f), 100);
        GameContextView.RenderManager.DrawSquare(new Vector2(200.0f, 200.0f), 100);
        GameContextView.RenderManager.DrawSquare(new Vector2(300.0f, 300.0f), 100);
        GameContextView.RenderManager.DrawSquare(new Vector2(400.0f, 400.0f), 100);
    }
}