using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.AssetSystem;
using FactorySimulator.Core.Source.ContentSystem;
using FactorySimulator.Core.Source.InputSystem;
using FactorySimulator.Core.Source.Level;
using FactorySimulator.Core.Source.RegistrySystem;
using Microsoft.Xna.Framework.Input;

namespace FactorySimulator.Core.Content;

[ContentModuleData("base")]
public class ContentModuleBase : ContentModule
{
    public override void RegisterContent(RegistryManager registryManager)
    {
        registryManager.Register(new LevelScene(BuildName("level")));

        registryManager.Register(new InputAction("up", [new KeyBinding(Keys.W)]));
        registryManager.Register(new InputAction("down", [new KeyBinding(Keys.S)]));
        registryManager.Register(new InputAction("left", [new KeyBinding(Keys.A)]));
        registryManager.Register(new InputAction("right", [new KeyBinding(Keys.D)]));
    }
}