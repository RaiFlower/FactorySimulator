using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorySimulator.Core.Source.SceneSystem;

public class SceneManager : Manager
{
    public SceneManager(GameContextView gameContextView) : base(gameContextView)
    {
        
    }

    private Scene _activeScene = null!;

    public override void Update()
    {
        base.Update();

        _activeScene.Update();
    }

    public override void Draw()
    {
        base.Draw();

        _activeScene.Draw();
    }

    public void SetActiveScene(string name)
    {
        Scene? scene = _gameContextView.RegistryManager.Scenes[name];

        if (_activeScene != null)
        {
            _activeScene.End();
        }

        _activeScene = scene;

        _activeScene.Start();
    }
}