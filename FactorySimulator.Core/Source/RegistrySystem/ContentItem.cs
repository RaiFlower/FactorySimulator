using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorySimulator.Core.Source.RegistrySystem;

public class ContentItem
{
    public readonly string Name;
    public GameContextView GameContextView = null!;

    public ContentItem(string name)
    {
        Name = name;
    }

    public void ProvideContextView(GameContextView gameContextView)
    {
        GameContextView = gameContextView;
    }
}