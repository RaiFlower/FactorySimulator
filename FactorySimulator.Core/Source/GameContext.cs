using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.AssetSystem;
using FactorySimulator.Core.Source.InputSystem;
using FactorySimulator.Core.Source.RegistrySystem;
using FactorySimulator.Core.Source.SceneSystem;

namespace FactorySimulator.Core.Source;

public interface IGameContext
{
    InputManager InputManager { get; }
    RenderManager RenderManager { get; }
    SceneManager SceneManager { get; }
    RegistryManager RegistryManager { get; }
    AssetManager AssetManager { get; }
}

public class GameContext : IGameContext
{
    public InputManager InputManager { get; set; } = null!;
    public RenderManager RenderManager { get; set; } = null!;
    public SceneManager SceneManager { get; set; } = null!;
    public RegistryManager RegistryManager { get; set; } = null!;
    public AssetManager AssetManager { get; set; } = null!;

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
}
