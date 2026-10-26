using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.AssetSystem;
using FactorySimulator.Core.Source.ContentSystem;
using FactorySimulator.Core.Source.Level;
using FactorySimulator.Core.Source.RegistrySystem;

namespace FactorySimulator.Core.Content;

[ContentModuleData("base")]
public class ContentModuleBase : ContentModule
{
    public override void RegisterContent(RegistryManager registryManager)
    {
        registryManager.Register(new LevelScene(BuildName("level")));
    }
}