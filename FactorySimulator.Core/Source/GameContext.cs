using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.AssetSystem;
using FactorySimulator.Core.Source.InputSystem;
using FactorySimulator.Core.Source.RegistrySystem;
using FactorySimulator.Core.Source.SceneSystem;
using Microsoft.Xna.Framework;

namespace FactorySimulator.Core.Source;

public interface IGameContext
{
    public InputManager InputManager { get; }
    public RenderManager RenderManager { get; }
    public SceneManager SceneManager { get; }
    public RegistryManager RegistryManager { get; }
    public AssetManager AssetManager { get; }

    public float Time { get; }
    public float DeltaTime { get; }
}

public class GameContext : IGameContext
{
    public InputManager InputManager { get; set; } = null!;
    public RenderManager RenderManager { get; set; } = null!;
    public SceneManager SceneManager { get; set; } = null!;
    public RegistryManager RegistryManager { get; set; } = null!;
    public AssetManager AssetManager { get; set; } = null!;

    public float Time { get; set; }
    public float DeltaTime { get; set; }

    public GameContextView BuildView()
    {
        return new(this);
    }
}

public class GameContextView : IGameContext
{
    private readonly GameContext _gameContext;

    public GameContextView(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public InputManager InputManager => _gameContext.InputManager;
    public RenderManager RenderManager => _gameContext.RenderManager;
    public SceneManager SceneManager => _gameContext.SceneManager;
    public RegistryManager RegistryManager => _gameContext.RegistryManager;
    public AssetManager AssetManager => _gameContext.AssetManager;

    public float Time => _gameContext.Time;
    public float DeltaTime => _gameContext.DeltaTime;
}
