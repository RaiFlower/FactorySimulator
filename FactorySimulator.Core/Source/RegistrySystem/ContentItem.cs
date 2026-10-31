using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorySimulator.Core.Source.RegistrySystem;

public class ContentItem
{
    public readonly string Name;
    protected GameContextView _gameContextView = null!;

    public ContentItem(string name)
    {
        Name = name;
    }

    public void ProvideContextView(GameContextView gameContextView)
    {
        _gameContextView = gameContextView;
    }

    public virtual void LoadContentReferences() { }
    
    public virtual void LoadAssetReferences() { }
}