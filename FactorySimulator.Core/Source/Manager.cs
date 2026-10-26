using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorySimulator.Core.Source;

public class Manager
{
    protected readonly GameContextView _gameContextView;

    public Manager(GameContextView gameContextView)
    {
        _gameContextView = gameContextView;
    }

    public virtual void Init() { }

    public virtual void Update() { }

    public virtual void Draw() { }
}